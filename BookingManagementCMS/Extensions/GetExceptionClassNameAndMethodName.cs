namespace CMS.Extensions;

public static class GetExceptionClassNameAndMethodName
{
    /// <summary>
    /// Get the class name and method name
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public static (string? ClassName, string? MethodName) GetActualMethodAndClassName(this Exception exception)
    {
        var stackTrace = new System.Diagnostics.StackTrace(exception, true);
        var frames = stackTrace.GetFrames();

        if (frames == null || frames.Length == 0)
            return (null, null);

        foreach (var frame in frames)
        {
            var method = frame.GetMethod();
            var declaringType = method?.DeclaringType;

            if (declaringType == null)
                continue;

            var fullClassName = declaringType.FullName ?? string.Empty;
            var methodName = method?.Name;

            // Skip framework/internal compiler-generated methods
            if (fullClassName.StartsWith("System.") || fullClassName.StartsWith("Microsoft."))
                continue;

            // Handle async state machine (MoveNext in a nested class)
            if (methodName == "MoveNext" && fullClassName.Contains("+<") && fullClassName.Contains(">d__"))
            {
                // Extract actual method name from generated class: ClassName+<MethodName>d__x
                string? actualMethodName = ExtractAsyncMethodNameFromGeneratedClassName(fullClassName);
                string? parentClassName = declaringType.DeclaringType?.FullName;

                if (!string.IsNullOrEmpty(parentClassName) && !string.IsNullOrEmpty(actualMethodName))
                    return (parentClassName, actualMethodName);
            }

            // Handle anonymous methods/lambdas like "<MethodName>b__0"
            if (methodName != null && methodName.StartsWith($"<") && methodName.Contains(">b__"))
            {
                var originalMethod = ExtractLambdaMethodName(methodName);
                return (fullClassName, originalMethod);
            }

            // Normal method, not compiler generated
            if (methodName != null && !methodName.StartsWith($"<") && methodName != "MoveNext")
            {
                return (fullClassName, methodName);
            }
        }

        return (null, null);
    }
    /// <summary>
    /// Get the class name
    /// </summary>
    /// <param name="className"></param>
    /// <returns></returns>
    private static string? ExtractAsyncMethodNameFromGeneratedClassName(string className)
    {
        int start = className.IndexOf("<") + 1;
        int end = className.IndexOf(">");
        if (start >= 0 && end > start)
            return className.Substring(start, end - start);

        return null;
    }
    /// <summary>
    /// Get the method name
    /// </summary>
    /// <param name="methodName"></param>
    /// <returns></returns>
    private static string ExtractLambdaMethodName(string methodName)
    {
        int start = methodName.IndexOf("<") + 1;
        int end = methodName.IndexOf(">");
        if (start >= 0 && end > start)
            return methodName.Substring(start, end - start) + " (lambda)";

        return methodName;
    }

}
