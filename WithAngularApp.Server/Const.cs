namespace WithAngularApp.Server
{
	public class Const
	{
		public enum Pattern {
			None,
			Solid, 
			Bicolor,
			Tabby,
			Colorpoint,
			Calico, 
			Tortoiseshell,
		}
		
		public enum Color { 
			None,
			White,
			Black, 
			Grey,
			Brown,
			Orange,
			Rainbow = 10
		}

		public enum Stat {
			None,
			Average,
			Good,
			Great,
			Perfect
		}
	}
}
