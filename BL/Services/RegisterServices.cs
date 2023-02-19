using AutoMapper;
using DAL;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace BL;

public class RegisterServices : IRegister
{
    #region Dependancy Injection
    private readonly IMapper _mapper;
    private readonly IPasspwordHasher _passpwordHasher;
    private readonly IClaimsInitializer _Calims;
    private readonly IJwtInitializer _jwtInitializer;
    private readonly UserContext _context;
    private readonly ICountry _country;
    public static int Varificationcode;

    public RegisterServices(IMapper mapper, UserContext context, ICountry country,
        IPasspwordHasher passpwordHasher, IClaimsInitializer calims, IJwtInitializer jwtInitializer)
    {
        _mapper = mapper;
        _context = context;
        _country = country;
        _passpwordHasher = passpwordHasher;
        _Calims = calims;
        _jwtInitializer = jwtInitializer;
    }
    #endregion
    public async Task<bool> UserRegesterAsync(AddUserDTO model)
    {
        var result = await GetUserByEmailAsync(model.Email);
        if (result != null)
            return false;

        using var dataStream = new MemoryStream();
        model.Photo.CopyTo(dataStream);

        var user = _mapper.Map<User>(model);
        user.Photo = dataStream.ToArray();

        user.Id = Guid.NewGuid();

        var country = user.country;
        var assignCountry = await _country.GetCountry(country.Code);
        user.country =  assignCountry;

        user.Role = "user";
        user.IsActive = true;
        user.Password = _passpwordHasher.PasswordHashing(model.Password);

        var claims = _Calims.CreateClaims(user);
        var token = _jwtInitializer.GenerateToken(claims);

        user.Token = token;

        await _context.Users.AddAsync(user);        
        await _context.SaveChangesAsync();
        Varificationcode = SendEmail(user.Email,user.FirstName);

        return true;
    }

    public async Task<bool> LoginAsync(LoginDTO model)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

        var hashPassword = _passpwordHasher.PasswordHashing(model.Password);
        if (hashPassword != user.Password)
            return false;

        var claims = _Calims.CreateClaims(user);
        var token = _jwtInitializer.GenerateToken(claims);

        user.Token = token;
        user.IsActive = true;

        _context.Users.Update(user);


        await _context.SaveChangesAsync();

        return true;
    }


    public async Task<bool> Verification(int verifyCode)
    {
        if (Varificationcode == verifyCode)
            return true;

        else
            return false;

    }


    public async Task<bool> LogoutAsync(Guid id)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

        user.IsActive = false;
        user.Token = null;


        _context.Update(user);
        await _context.SaveChangesAsync();

        return true;
    }



    #region Helping Methods
    public async Task<ReadUserDTO> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null)
            return null;

        var userRead = _mapper.Map<ReadUserDTO>(user);
        return userRead;
    }

    public static int SendEmail(string userMail, string userName)
    {
        int _min = 1000;
        int _max = 9999;
        Random _rdm = new Random();
        var num = _rdm.Next(_min, _max);

        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse("testjust971@gmail.com"));
        email.To.Add(MailboxAddress.Parse(userMail));
        email.Subject = "Confirmation Code";
        var body = "Hello " + userName + "! Welcome To our app here is your confirmation code "+ num;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

        using var smtp = new SmtpClient();
        smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
        smtp.Authenticate("testjust971@gmail.com", "sndnssvqyfeicrud");

        smtp.Send(email);
        smtp.Disconnect(true);
        return num;
    }

    #endregion
}
