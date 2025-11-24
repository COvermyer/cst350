namespace ButtonGrid.Models
{
	public class ButtonModel
	{
		public int Id { get; set; }
		public int ButtonState { get; set; }
		public string ButtonImage { get; set; }

		public ButtonModel(int id, int buttonState, string buttonImg)
		{ // Parameterized Constructor
			Id = id;
			ButtonState = buttonState;
			ButtonImage = buttonImg;
		}

		public ButtonModel()
		{
			// Default constructor
		}
	}
}
