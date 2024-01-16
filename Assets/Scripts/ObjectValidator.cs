using UnityEngine;

public static class ObjectValidator
{
    /// <summary>
    /// Checks if the specified object is null and logs a warning message if it is.
    /// </summary>
    /// <param name="obj">The object to check for null</param>
    /// <param name="logWarningMessage">The warning message to log if the object is null</param>
    /// <returns>
    ///     <c>true</c> if the object is null; otherwise, <c>false</c>
    /// </returns>
    public static bool IsObjectNull(object obj, string logWarningMessage)
    {
        if (obj == null)
        {
            Debug.LogWarning(logWarningMessage);
            return true;
        }

        return false;
    }
}
