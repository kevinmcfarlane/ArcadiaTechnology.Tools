using System.Configuration;

namespace ArcadiaTechnology.Tools
{
    /// <summary>
    /// Encrypts or decrypts a Configuration file section.
    /// </summary>
    public static class Encrypter
    {
        /// <summary>
        /// Marks a configuration section for protection.
        /// </summary>
        /// <param name="sectionName">The path to the section.</param>
        /// <param name="provider">The name of the protection provider to use.</param>
        public static void ProtectSection(string sectionName, string provider)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            // For  web version use this - we should move these calls out to client really
            // Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);

            ProtectSectionImpl(sectionName, provider, config);
        }

        /// <summary>
        /// Marks a configuration section for protection.
        /// </summary>
        /// <param name="sectionName">The path to the section.</param>
        /// <param name="provider">The name of the protection provider to use.</param>
        /// <param name="config">Represents an application or web configuration file.</param>
        /// <example>
        /// For App.config use:
        /// <code>
        /// Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        /// ProtectSection("configuration/appSettings", "RsaProtectedConfigurationProvider", config);
        /// </code>
        /// For Web.config use:
        /// <code>
        /// Configuration config = WebConfigurationManager.OpenWebConfiguration(Request.ApplicationPath);
        /// ProtectSection("appSettings", "RsaProtectedConfigurationProvider", config);
        /// </code>
        /// </example>
        public static void ProtectSection(string sectionName, string provider, Configuration config)
        {
            ProtectSectionImpl(sectionName, provider, config);
        }

        /// <summary>
        /// Removes the protected configuration encryption from the associated configuration section.
        /// </summary>
        /// <param name="sectionName">The path to the section.</param>
        public static void UnprotectSection(string sectionName)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            UnprotectSectionImpl(sectionName, config);
        }

        private static void ProtectSectionImpl(string sectionName, string provider, Configuration config)
        {
            ConfigurationSection section = config.GetSection(sectionName);

            if (section != null && !section.SectionInformation.IsProtected)
            {
                section.SectionInformation.ProtectSection(provider);
                config.Save();
            }
        }

        private static void UnprotectSectionImpl(string sectionName, Configuration config)
        {
            ConfigurationSection section = config.GetSection(sectionName);

            if (section != null && section.SectionInformation.IsProtected)
            {
                section.SectionInformation.UnprotectSection();
                config.Save();
            }
        }
    }
}