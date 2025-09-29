# Parabank BDD Automation

This project demonstrates a robust BDD-style automation framework that automates end-to-end testing of the Parabank web application in **C#** using **Playwright**, **Reqnroll (SpecFlow fork)**, and **BDD-style step definitions**. It showcases modular design, reusable components that includes resilient fallback logic, screenshot capture, parallel execution, cross-browser configuration, externalized credentials, **CI integration via GitHub** Actions and Performance testing using **K6**.

---

## 🎯 Project Objectives

1. **Framework Setup**  
   - Built a lightweight BDD automation framework in C#  
   - Integrated Playwright for browser automation  
   - Applied Page Object Model (POM) for maintainability

2. **Scenario Design (Gherkin)**  
   - Designed 5 meaningful scenarios reflecting critical ParaBank flows  
   - Scenarios include login, account overview, transaction overview, and fund transfer  
   - Written in clear, business-readable Gherkin syntax

3. **Implementation**  
   - Step definitions implemented using Playwright + C#  
   - Assertions validate key outcomes (e.g., login success, transaction confirmation)  
   - Fallback logic and screenshot capture added for resilience

4. **Test Data Management**  
   - Credentials and config externalized via `appsettings.json`  
   - Reused test data across scenarios using shared context

5. **Reporting**  
   - Generates `.trx` logs and optional ExtentReport HTML  
   - Screenshots captured on failure or fallback  and success
   - CI artifacts uploaded via GitHub Actions
   - Performance testing via K6

---

## 🚀 Setup Instructions

### Prerequisites

- [.NET SDK 8.0+](https://dotnet.microsoft.com/en-us/download)
- [Node.js (for Playwright CLI, K6)](https://nodejs.org/)
- Git (for version control)
- Visual Studio 2022+ (recommended)

### Install Dependencies

   ```bash
   dotnet restore
   dotnet build --configuration Release
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install
   ```

### Installation Steps

1. Clone the repository:
   ```bash
   git clone https://github.com/Tina879-cmd/ParabankBDDAutomation.git
   cd ParabankBDDAutomation

2. Restore and build the solution:
   dotnet restore
   dotnet build --configuration Release

3. Install Playwright CLI and browser binaries:
   dotnet tool install --global Microsoft.Playwright.CLI
   playwright install


### Test Execution

#### Locally

```bash
dotnet restore
dotnet build --configuration Release
dotnet tool install --global Microsoft.Playwright.CLI
playwright install
dotnet test --settings test.runsettings --logger "trx"
```

1. Run all tests with parallel execution and logging:
    ```bash
    dotnet test --settings test.runsettings --logger "trx"
    ```

2. Artifacts:
   .trx test results saved in /TestResults
   Screenshots saved in /Screenshots on failure or fallback
   ExtentReport saved in /Reports

##### CI (GitHub Actions)

1. Push to master or open a pull request. The workflow in .github/workflows/test.yml will:
   a. Build the solution
   b. Install Playwright CLI and browsers
   c. Run tests with fallback logic
   d. Upload screenshots, .trx logs, and ExtentReport

### Performance Test Execution

#### Locally
To run the test locally:

1. **Install K6**  
   - Via [Node.js](https://nodejs.org/en/download):  
     ```bash
     npm install -g k6
     ```
   - Or via [Chocolatey](https://community.chocolatey.org/packages/k6):  
     ```bash
     choco install k6
     ```

2. **Execute the test**  
   ```bash
   k6 run ParabankBDDAutomation/Performance/login-test.js
   ```

#### CI Integration

K6 is integrated into GitHub Actions via `.github/workflows/performance.yml`. On every push to `master` or manual trigger:

- K6 runs the login performance test
- A summary report is exported to `k6-results/summary.json`
- Artifacts are uploaded for review under **Actions → K6 Performance Test**

> ✅ This ensures performance regressions are caught early and metrics are traceable across builds.

---

### 📈 Sample Metrics Captured

- ✅ Request duration
- ✅ Success rate
- ✅ Failed checks
- ✅ Throughput (requests/sec)

---

## 📦 Functional Automation

The framework supports:

- Playwright-based UI automation
- SpecFlow/Reqnroll step definitions
- Parallel execution, cross-browser configuration, externalized credentials
- ExtentReports with embedded screenshots
- CI-ready execution with parallel support
- CI-ready execution with Performance testing
  
---

## 🧾 Reporting

Test results are saved to:

```
Reports/ExtentReport.html
Reports/Screenshots/
```

Screenshots are embedded per step and uploaded as CI artifacts for traceability.

---



### Project Structure
   ```bash
   ParabankBDDAutomation/
   ├── Pages/                 # Page object models
   ├── Steps/                 # Step definitions
   ├── Hooks/                 # Screenshot + error handling
   ├── Support/               # Framework utilities and configuration
   │   ├── ConfigLoader.cs           # Loads config from appsettings.json or environment
   │   ├── ReportManager.cs          # Initializes and flushes ExtentReports
   │   ├── TestContext.cs            # Shared context for browser, page, scenario metadata
   │   ├── TestSettings.cs           # Strongly typed config model (e.g., baseUrl, timeout)
   │   └── CredentialSettings.cs     # Secure model for username/password or token-based auth
   ├── Reports/               # ExtentReport.html (optional)
   ├── Screenshots/           # Captured on failure or fallback
   ├── Performance/           # Performance scripts using K6
   ├── test.runsettings       # Parallel execution config
   └── ParabankBDDAutomation.csproj
   ```

### Assumptions & Limitations

1. Tests assume stable access to the Parabank demo environment, which is known to be intermittently unstable.
2. Test failures may occur due to environmental instability, including server timeouts or inconsistent page loads.
3. Error messages are sometimes incorrect when invalid credentials are used — the UI may not reflect the expected failure state.
4. Account details change even when using the same credentials repeatedly, making data validation unreliable across sessions.
5. User registration is not persistent — even after successful registration, the account may disappear, requiring re-registration.
6. After re-registration, account details are reset or altered, which affects test repeatability and data consistency.
7. Screenshots and fallback logic are used to capture and trace these inconsistencies for debugging and reporting.
8. CI assumes browser binaries are installed via playwright install.
9. GitHub Actions runner uses windows-latest.
10. GitHub Secrets Vault was intentionally not used to avoid requiring personal tokens or credentials for others to run the project.
    - All configuration and credentials are externalized via appsettings.json for transparency and ease of sharing.
    - This makes the project fully runnable without needing access to private secrets or CI tokens.




    
