using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

public static class ReportManager
{
    public static ExtentReports Extent;
    public static ExtentTest Test;

    public static void InitReport()
    {
        var sparkReporter = new ExtentSparkReporter("ExtentReport.html");
        Extent = new ExtentReports();
        Extent.AttachReporter(sparkReporter);
    }

    public static void FlushReport() => Extent.Flush();
}
