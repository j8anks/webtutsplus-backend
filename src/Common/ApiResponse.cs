

	public class ApiResponse
	{
		private bool success;
		private String message;
	// try again
	
	public ApiResponse(bool success, String message)
		{
			this.success = success;
			this.message = message;
		}

		public bool isSuccess()
		{
			return success;
		}

		public String getMessage()
		{
			return message;
		}

		public String getTimestamp()
		{
			return DateTime.Now.ToString();
		}
	}

