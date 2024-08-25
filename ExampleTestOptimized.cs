using Allure.NUnit;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
[AllureNUnit]
public class ExampleTestOptimized : PageTest
{
    private string _baseUrl;

    [SetUp]
    public async Task SetUp()
    {
        _baseUrl = Environment.GetEnvironmentVariable("BASEURL");
        await Page.GotoAsync(_baseUrl);
    }

    private async Task FillForm(string firstName = "ValidFirstName", string surname = "ValidLastName", string age = "45", string country = "Greece", string notes = "these are some test notes")
    {
        await Page.Locator("#firstname").FillAsync(firstName);
        await Page.Locator("#surname").FillAsync(surname);
        await Page.Locator("#age").FillAsync(age);
        await Page.SelectOptionAsync("#country", country);
        await Page.Locator("#notes").FillAsync(notes);
    }

    private async Task SubmitForm()
    {
        await Page.Locator("input[type='submit']").ClickAsync();
    }

    [Test]
    public async Task SignInWithValidCredentials()
    {
        await FillForm();
        await SubmitForm();
        await Expect(Page.GetByText("Your Input passed validation")).ToBeVisibleAsync();
    }

    [Test]
    public async Task SignInWithValidFirstnameAndInvalidSurname()
    {
        await FillForm(surname: "Doe");
        await SubmitForm();
        await Expect(Page.GetByText("Surname provided is too short")).ToBeVisibleAsync();
    }

    [Test]
    public async Task SignInWithInvalidFirstnameAndValidSurname()
    {
        await FillForm(firstName: "Jo");
        await SubmitForm();
        await Expect(Page.GetByText("Firstname provided is too short")).ToBeVisibleAsync();
    }
    [Test]
    public async Task SignInWithInvalidFirstnameAndInvalidSurname()
    {
        await FillForm(firstName: "Jo", surname: "Doe");
        await SubmitForm();
        await Task.WhenAll(
            Expect(Page.GetByText("Surname provided is too short")).ToBeVisibleAsync(),
            Expect(Page.GetByText("Firstname provided is too short")).ToBeVisibleAsync()
        );

    }
}
