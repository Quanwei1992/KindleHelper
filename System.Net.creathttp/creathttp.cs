using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net.Configuration;
using System.Security;
using System.Security.Permissions;
using System.Configuration;
using System.Reflection;
using System.Globalization;
namespace System.Net
{
    public class creathttp
    {
        // For portability
        public static HttpWebRequest CreateHttp(string requestUriString)
        {
            if (requestUriString == null)
            {
                throw new ArgumentNullException("requestUriString");
            }
            return CreateHttp(new Uri(requestUriString));
        }

        // For portability
        public static HttpWebRequest CreateHttp(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            if ((requestUri.Scheme != Uri.UriSchemeHttp) && (requestUri.Scheme != Uri.UriSchemeHttps))
            {
                throw new NotSupportedException();
            }
            return (HttpWebRequest)CreateDefault(requestUri);
        }
        public static WebRequest CreateDefault(Uri requestUri)
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException("requestUri");
            }
            // In .NET FX v4.0, custom IWebRequestCreate implementations can 
            // cause this to return null.  Consider tightening this in the future.
            //Contract.Ensures(Contract.Result<WebRequest>() != null);

            return Create(requestUri, true);
        }
        private static WebRequest Create(Uri requestUri, bool useUriBase)
        {
            string LookupUri;
            WebRequestPrefixElement Current = null;
            bool Found = false;

            if (!useUriBase)
            {
                LookupUri = requestUri.AbsoluteUri;
            }
            else
            {

                //
                // schemes are registered as <schemeName>":", so add the separator
                // to the string returned from the Uri object
                //

                LookupUri = requestUri.Scheme + ':';
            }

            int LookupLength = LookupUri.Length;

            // Copy the prefix list so that if it is updated it will
            // not affect us on this thread.

            ArrayList prefixList = PrefixList;

            // Look for the longest matching prefix.

            // Walk down the list of prefixes. The prefixes are kept longest
            // first. When we find a prefix that is shorter or the same size
            // as this Uri, we'll do a compare to see if they match. If they
            // do we'll break out of the loop and call the creator.

            for (int i = 0; i < prefixList.Count; i++)
            {
                Current = (WebRequestPrefixElement)prefixList[i];

                //
                // See if this prefix is short enough.
                //

                if (LookupLength >= Current.Prefix.Length)
                {

                    //
                    // It is. See if these match.
                    //

                    if (String.Compare(Current.Prefix,
                                       0,
                                       LookupUri,
                                       0,
                                       Current.Prefix.Length,
                                       StringComparison.OrdinalIgnoreCase) == 0)
                    {

                        //
                        // These match. Remember that we found it and break
                        // out.
                        //

                        Found = true;
                        break;
                    }
                }
            }

            WebRequest webRequest = null;

            if (Found)
            {

                //
                // We found a match, so just call the creator and return what it
                // does.
                //

                webRequest = Current.Creator.Create(requestUri);
                return webRequest;
            }
            return null;
        }
        private static volatile ArrayList s_PrefixList;
        private static Object s_InternalSyncObject;
        private static Object InternalSyncObject
        {
            get
            {
                if (s_InternalSyncObject == null)
                {
                    s_InternalSyncObject = new object();
                }
                return s_InternalSyncObject;
            }
        }
        static internal WebRequestModulesSectionInternal GetSection()
        {
            lock (WebRequestModulesSectionInternal.ClassSyncObject)
            {
                WebRequestModulesSection section = PrivilegedConfigurationManager.GetSection(WebRequestModulesSectionPath) as WebRequestModulesSection;
                if (section == null)
                    return null;

                return new WebRequestModulesSectionInternal(section);
            }
        }
        internal static ArrayList PrefixList
        {

            get
            {
                //
                // GetConfig() might use us, so we have a circular dependency issue,
                // that causes us to nest here, we grab the lock, only
                // if we haven't initialized.
                //
                if (s_PrefixList == null)
                {

                    lock (InternalSyncObject)
                    {
                        if (s_PrefixList == null)
                        {
                            s_PrefixList = GetSection().WebRequestModules; ;
                        }
                    }
                }

                return s_PrefixList;
            }
            set
            {
                s_PrefixList = value;
            }
        }
        internal sealed class WebRequestModulesSectionInternal
        {
            internal WebRequestModulesSectionInternal(WebRequestModulesSection section)
            {
                if (section.WebRequestModules.Count > 0)
                {
                    this.webRequestModules = new ArrayList(section.WebRequestModules.Count);
                    foreach (WebRequestModuleElement webRequestModuleElement in section.WebRequestModules)
                    {
                        try
                        {
                            this.webRequestModules.Add(new WebRequestPrefixElement(webRequestModuleElement.Prefix, webRequestModuleElement.Type));
                        }
                        catch
                        {
                            throw;
                        }
                    }
                }
            }

            internal static object ClassSyncObject
            {
                get
                {
                    if (classSyncObject == null)
                    {
                        classSyncObject = new object();
                    }
                    return classSyncObject;
                }
            }

            static internal WebRequestModulesSectionInternal GetSection()
            {
                lock (WebRequestModulesSectionInternal.ClassSyncObject)
                {
                    WebRequestModulesSection section = PrivilegedConfigurationManager.GetSection(WebRequestModulesSectionPath) as WebRequestModulesSection;
                    if (section == null)
                        return null;

                    return new WebRequestModulesSectionInternal(section);
                }
            }

            internal ArrayList WebRequestModules
            {
                get
                {
                    ArrayList retval = this.webRequestModules;
                    if (retval == null)
                    {
                        retval = new ArrayList(0);
                    }
                    return retval;
                }
            }

            static object classSyncObject = null;
            ArrayList webRequestModules = null;
        }
        internal const string WebRequestModulesSectionName = "webRequestModules";
        internal const string SectionGroupName = "system.net";
        static internal string WebRequestModulesSectionPath
        {
            get { return GetSectionPath(WebRequestModulesSectionName); }
        }
        static string GetSectionPath(string sectionName)
        {
            return string.Format(CultureInfo.InvariantCulture, @"{0}/{1}", SectionGroupName, sectionName);
        }
        internal static class PrivilegedConfigurationManager
        {
            internal static ConnectionStringSettingsCollection ConnectionStrings
            {
                [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
                get
                {
                    return ConfigurationManager.ConnectionStrings;
                }
            }

            internal static object GetSection(string sectionName)
            {
                return ConfigurationManager.GetSection(sectionName);
            }
        }
        internal class WebRequestPrefixElement
        {

            /// <devdoc>
            ///    <para>[To be supplied.]</para>
            /// </devdoc>
            public string Prefix;
            /// <devdoc>
            ///    <para>[To be supplied.]</para>
            /// </devdoc>
            internal IWebRequestCreate creator;
            /// <devdoc>
            ///    <para>[To be supplied.]</para>
            /// </devdoc>
            internal Type creatorType;

            public IWebRequestCreate Creator
            {
                get
                {
                    if (creator == null && creatorType != null)
                    {
                        lock (this)
                        {
                            if (creator == null)
                            {
                                creator = (IWebRequestCreate)Activator.CreateInstance(
                                                            creatorType,
                                                            BindingFlags.CreateInstance
                                                            | BindingFlags.Instance
                                                            | BindingFlags.NonPublic
                                                            | BindingFlags.Public,
                                                            null,          // Binder
                                                            new object[0], // no arguments
                                                            CultureInfo.InvariantCulture
                                                            );
                            }
                        }
                    }

                    return creator;
                }

                set
                {
                    creator = value;
                }
            }

            public WebRequestPrefixElement(string P, Type creatorType)
            {
                // verify that its of the proper type of IWebRequestCreate
                if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
                {
                    throw new InvalidCastException();
                }

                Prefix = P;
                this.creatorType = creatorType;
            }

            /// <devdoc>
            ///    <para>[To be supplied.]</para>
            /// </devdoc>
            public WebRequestPrefixElement(string P, IWebRequestCreate C)
            {
                Prefix = P;
                Creator = C;
            }

        } // class PrefixListElement
    }
 
}
