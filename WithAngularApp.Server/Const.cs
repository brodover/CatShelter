namespace WithAngularApp.Server
{
	public class Const
	{
		public const double RainbowProb = 0.05d;

		public enum Pattern {
			NA,
			Solid, 
			Bicolor,
			Tabby,
			Colorpoint,
			Calico, 
			Tortoiseshell,
		}
		
		public enum Color {
			NA,
			White,
			Black, 
			Grey,
			Brown,
			Orange,
			Rainbow = 10
		}

		public enum Stat {
			NA,
			Average,
			Good,
			Great,
			Perfect
		}
	}
}
