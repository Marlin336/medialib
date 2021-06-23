namespace Medialib_proj
{
	public enum Added_item { movie_g, music_g, picture_g, text_g, movie, music, picture, text, person, album, band };
    public class Reference
    {
		public int id { get; set; }
		public object value { get; set; }
		public Reference(int id, object value)
		{
			this.id = id;
			this.value = value;
		}
    }
}
