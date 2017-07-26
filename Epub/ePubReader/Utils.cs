using System.Text.RegularExpressions;

namespace eBdb.EpubReader {
	public class Utils {
		public static readonly RegexOptions REO_ = RegexOptions.CultureInvariant | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture;
		public static readonly RegexOptions REO_c = RegexOptions.Compiled | REO_;
		public static readonly RegexOptions REO_s = RegexOptions.Singleline | REO_;
		public static readonly RegexOptions REO_cs = RegexOptions.Compiled | REO_s;
		public static readonly RegexOptions REO_m = RegexOptions.Multiline | REO_;
		public static readonly RegexOptions REO_cm = RegexOptions.Compiled | REO_m;
		public static readonly RegexOptions REO_i = RegexOptions.IgnoreCase | REO_;
		public static readonly RegexOptions REO_ci = RegexOptions.IgnoreCase | REO_c;
		public static readonly RegexOptions REO_si = RegexOptions.Singleline | REO_i;
		public static readonly RegexOptions REO_csi = RegexOptions.Compiled | REO_si;
		public static readonly RegexOptions REO_mi = RegexOptions.Multiline | REO_i;
		public static readonly RegexOptions REO_cmi = RegexOptions.Compiled | REO_mi;
		public static readonly RegexOptions REO_wsi = REO_si ^ RegexOptions.IgnorePatternWhitespace;
		public static readonly RegexOptions REO_wcm = REO_cm ^ RegexOptions.IgnorePatternWhitespace;

		public static string ClearText(string text) {
			if (text == null) return null;
			string result = ReplaceBlockTagsToNewLineCharacter(text);
			return ClearSpecialSymbols(CleanHtmlTags(result));
		}

		public static string CleanHtmlTags(string text) {
			if (text == null) return null;
			return Regex.Replace(text, @"</?(\w+|\s*!--)[^>]*>", " ", REO_c);
		}

		private static string ReplaceBlockTagsToNewLineCharacter(string text) {
			if (text == null) return null;
			return Regex.Replace(text, @"(?<!^\s*)<(p|div|h1|h2|h3|h4|h5|h6)[^>]*>", "\n", REO_cmi);
		}

		private static string ClearSpecialSymbols(string text) {
			if (text == null) return null;
			Regex regex = new Regex(@"(?<defined>(&nbsp|&quot|&mdash|&ldquo|&rdquo|\&\#8211|\&\#8212|&\#8230|\&\#171|&laquo|&raquo|&amp);?)|(?<other>\&\#\d+;?)", REO_ci);
			return Regex.Replace(regex.Replace(text, SpecialSymbolsEvaluator), @"\ {2,}", " ", REO_c);
		}

		private static string SpecialSymbolsEvaluator(Match m) {
			if (m.Groups["defined"].Success) {
				switch (m.Groups["defined"].Value.ToLower()) {
					case "&nbsp;": return " ";
					case "&nbsp": return " ";
					case "&quot;": return "\"";
					case "&quot": return "\"";
					case "&mdash;": return " ";
					case "&mdash": return " ";
					case "&ldquo;": return "\"";
					case "&ldquo": return "\"";
					case "&rdquo;": return "\"";
					case "&rdquo": return "\"";
					case "&#8211;": return "-";
					case "&#8211": return "-";
					case "&#8212;": return "-";
					case "&#8212": return "-";
					case "&#8230": return "...";
					case "&#171;": return "\"";
					case "&#171": return "\"";
					case "&laquo;": return "\"";
					case "&laquo": return "\"";
					case "&raquo;": return "\"";
					case "&raquo": return "\"";
					case "&amp;": return "&";
					case "&amp": return "&";
					default: return " ";
				}
			}
			return " ";
		}
	}
}
