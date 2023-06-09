﻿using StixVuln.Core;
using StixVuln.Core.Vulnerability;

using Xunit;

namespace StixVuln.UnitTests.ExternalReferencesTest;
public class ExternalReferences
{

    private readonly string vulnName = "Vuln1";
    private readonly string vulnDesc = "This is a new vulnerability";
    private readonly string exSourceName = "ACME Threat Intel";
    private readonly string exDescription = "Threat report";
    private readonly string exExternalId = "1370";
    private readonly string exUrl = "https://google.com";
    private readonly string exUrlHash = "6db12788c37247f2316052e142f42f4b259d6561751e5f401a1ae2a6df9c674b";

    [Fact]
    public void ShouldCreateNewExternalReference_GivenCorrectParameters()
    {
        var exRefCreation = () => new ExternalReference(
            exSourceName,
            exDescription,
            exExternalId,
            exUrl,
            exUrlHash);
        Assert.True(true);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_GivenExternalReferenceEmptySourceName()
    {
        var exRefCreation = () => new ExternalReference(
            "",
            exDescription,
            exExternalId,
            exUrl,
            exUrlHash);
        Assert.Throws<ArgumentNullException>(exRefCreation);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_GivenExternalReferenceMissingProperties()
    {
        var exRefCreation = () => new ExternalReference(
            exSourceName,
            "",
            "",
            "",
            null);
        Assert.Throws<ArgumentNullException>(exRefCreation);
    }

    [Fact]
    public void ShouldThrowArgumentNullException_GivenUrlMissingHash()
    {
        var exRefCreation = () => new ExternalReference(
            exSourceName,
            exDescription,
            exExternalId,
            exUrl,
            null);
        Assert.Throws<ArgumentNullException>(exRefCreation);
    }

    [Fact]
    public void ShouldContainExternalReference_AddingExternalReference_GivenCorrectParameters()
    {
        var vuln = new Vulnerability(vulnName, vulnDesc, null);

        var exRef = new ExternalReference(
            exSourceName,
            exDescription,
            exExternalId,
            null,
            null);

        vuln.SetExternalReferences(new List<ExternalReference> { exRef });

        // TODO: Implement equality for external reference objects
        Assert.True(vuln.ExternalReferences.Any(er => er.SourceName == exSourceName));
    }
}
