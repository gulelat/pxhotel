using System.Linq;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel.Repositories.RepositoryBase;

namespace PX.EntityModel.Repositories
{
    public class LocalizedResourceRepository : Repository<LocalizedResource>
    {
        /// <summary>
        /// Get localized resource by key and language
        /// </summary>
        /// <param name="languageKey">the language</param>
        /// <param name="textKey">the text</param>
        /// <returns></returns>
        public static LocalizedResource Get(string languageKey, string textKey)
        {
            return FetchFirst(l => l.TextKey.Equals(textKey) && l.LanguageId.Equals(languageKey));
        }

        /// <summary>
        /// Insert a new override text using the site language
        /// </summary>
        /// <param name="textKey">The text to be overridden</param>
        /// <param name="defaultText">The default value of text</param>
        /// <param name="overrideText">Text to override</param>
        /// <param name="languageKey">Language id for the text</param>
        /// <returns>True if successful, otherwise false</returns>
        public static ResponseModel Insert(string textKey, string defaultText, string overrideText, string languageKey)
        {
            var textToEdit = GetAll().FirstOrDefault(x => x.LanguageId.Equals(languageKey) && x.TextKey.Equals(textKey));
            if (textToEdit == null)
            {
                var languageText = new LocalizedResource
                {
                    TranslatedValue = overrideText,
                    TextKey = textKey,
                    DefaultValue = defaultText,
                    LanguageId = languageKey
                };
                return Insert(languageText);
            }

            // Don't need to update the override text if the new text is equal
            if (defaultText.Equals(textToEdit.DefaultValue) && overrideText.Equals(textToEdit.TranslatedValue))
                return new ResponseModel
                {
                    Success = true,
                    Data = defaultText
                };

            textToEdit.DefaultValue = defaultText;
            textToEdit.TranslatedValue = overrideText;
            return Update(textToEdit);
        }
    }
}
