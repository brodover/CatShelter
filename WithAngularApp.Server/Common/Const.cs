namespace WithAngularApp.Server.Common
{
    public class Const
    {
		public class Server
		{
			public enum Provider
			{
				None,
				Google
			}
		}

        public class Game
        {
			public const double RainbowProb = 0.05d;

			public enum Pattern
			{
				NA,
				Solid,
				Bicolor,
				Tabby,
				Colorpoint,
				Calico,
				Tortoiseshell,
			}

			public enum Color
			{
				NA,
				White,
				Black,
				Grey,
				Brown,
				Orange,
				Rainbow = 10
			}

			public enum Stat
			{
				NA,
				Average,
				Good,
				Great,
				Perfect
			}
		}
        
    }
}
