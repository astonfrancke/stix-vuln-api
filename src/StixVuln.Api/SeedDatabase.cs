using Microsoft.EntityFrameworkCore;

using StixVuln.Core;
using StixVuln.Core.Authentication;
using StixVuln.Core.Identity;
using StixVuln.Core.Vulnerability;
using StixVuln.Infrastructure.Data;

namespace StixVuln.Api;

public static class SeedDatabase
{


    public static void Initialize(IServiceProvider serviceProvider)
    {
        using var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());
        if (
            dbContext.Vulnerabilities.Any() &&
            dbContext.Identitites.Any() &&
            dbContext.Users.Any())
        {
            //return;   // Db already seeded
        }

        PopulateTestData(dbContext);
    }
    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Vulnerabilities)
        {
            dbContext.Remove(item);
        }

        foreach (var item in dbContext.Identitites)
        {
            dbContext.Remove(item);
        }

        foreach (var item in dbContext.Users)
        {
            dbContext.Remove(item);
        }


        dbContext.SaveChanges();


        var id1 = new Identity(
                "John Smith",
                "",
                "123456789",
                "individual",
                new List<string> { },
                new List<string> { });
        var id2 = new Identity(
                "ACME Widget, Inc.",
                "",
                "acme@test.com",
                "organization",
                new List<string> { "Admin" },
                new List<string> { });

        dbContext.Identitites.Add(id1);
        dbContext.Identitites.Add(id2);

        dbContext.SaveChanges();

        var user1 = new User("user1", "password1", id1.Id, Core.Authentication.Role.User);
        var admin1 = new User("admin1", "password1", id2.Id, Core.Authentication.Role.Admin);
        
        dbContext.Users.Add(user1);
        dbContext.Users.Add(admin1);

        dbContext.SaveChanges();

        var vuln1 = new Vulnerability(
                "CVE-2016-1234",
                "Critical vulnerability",
                id1.Id);
        var vuln2 = new Vulnerability(
                "CVE-2018-1234",
                "Minor vulnerability",
                id2.Id);
        var vuln3 = new Vulnerability(
                "CVE-2023-1111",
                "Dangerous flaw",
                id2.Id);
        var exRef1 = new ExternalReference(
                "ACME Threat Intel",
                "Threat report",
                "1370",
                "https://google.com",
                "6db12788c37247f2316052e142f42f4b259d6561751e5f401a1ae2a6df9c674b");
        var exRef2 = new ExternalReference(
                "capec",
                "",
                "CAPEC-550",
                "http://capec.mitre.org/data/definitions/550.html",
                "6db12788c37247f2316052eweaewae4b259d6561751e5f401a1ae2a6df9c674b");
        var exRef3 = new ExternalReference(
                 "ACME Bugzilla",
                 "Bug incident",
                 "13702_bug",
                 "https://www.example.com/bugs/13702_bug",
                 "6db12788c37247f2316052eweaewae4b259d6561751e5a32vbv090a6df9c674b");

        vuln1.SetExternalReferences(new List<ExternalReference> { exRef1, exRef2 });
        vuln2.SetExternalReferences(new List<ExternalReference> { exRef3 });

        dbContext.Vulnerabilities.Add(vuln1);
        dbContext.Vulnerabilities.Add(vuln2);
        dbContext.Vulnerabilities.Add(vuln3);

        dbContext.SaveChanges();
    }
}
