﻿using System;

using Microsoft.EntityFrameworkCore;

using StixVuln.Core.Vulnerability;
using StixVuln.Infrastructure.Data;

namespace StixVuln.Api;

public static class SeedDatabase
{
    public static readonly Vulnerability Vuln1 = new("CVE-2016-1234", "Critical vulnerability", null);
    public static readonly Vulnerability Vuln2 = new("CVE-2018-1234", "Minor vulnerability", null);

    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var dbContext = new AppDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
        {
            if (dbContext.Vulnerabilities.Any())
            {
                return;   // DB has been seeded
            }

            PopulateTestData(dbContext);
        }
    }
    public static void PopulateTestData(AppDbContext dbContext)
    {
        foreach (var item in dbContext.Vulnerabilities)
        {
            dbContext.Remove(item);
        }

        dbContext.SaveChanges();

        Vuln1.AddExternalReference(
            "ACME Threat Intel",
            "Threat report",
            "1370",
            "https://google.com",
            "6db12788c37247f2316052e142f42f4b259d6561751e5f401a1ae2a6df9c674b");

        Vuln1.AddExternalReference(
            "capec",
            "",
            "CAPEC-550",
            "http://capec.mitre.org/data/definitions/550.html",
            "6db12788c37247f2316052eweaewae4b259d6561751e5f401a1ae2a6df9c674b");

        Vuln2.AddExternalReference(
            "ACME Bugzilla",
            "Bug incident",
            "13702_bug",
            "https://www.example.com/bugs/13702_bug",
            "6db12788c37247f2316052eweaewae4b259d6561751e5a32vbv090a6df9c674b");

        dbContext.Vulnerabilities.Add(Vuln1);
        dbContext.Vulnerabilities.Add(Vuln2);

        dbContext.SaveChanges();
    }
}