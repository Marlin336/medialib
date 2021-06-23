using Npgsql;
using System.Collections.Generic;

namespace Medialib_proj
{
	public static class Shared_data
	{
		public static string user_name { get; set; }
		public static List<string> perm_list { get; set; } = new List<string>();
		public static NpgsqlConnection conn { get; set; }
		public static int lang_id { get; set; }
	}
}
