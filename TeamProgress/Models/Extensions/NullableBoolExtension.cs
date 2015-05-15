namespace ADM
{
    public static class NullableBoolExtension
    {
        /// <summary>
        ///     ObjectExtension
        ///        
        ///     Author              Company                             Date            Description
        ///     ------------------- ----------------------------------- --------------- -----------------------------------------
        ///     Regis Charlot       Intelligent Medical Objects, Inc.   July 22, 2013  Inital Creation
        ///     
        /// </summary>

        /// <summary>
        ///     ToString
        ///     
        /// </summary>
        public static string ToString(this bool? value)
        {
            switch (value)
            {
                case true:
                    return "true";
                case false:
                    return "true";
                default:
                    return "<null>";
            }
        }
    }
}
