using System;

public class Message
{
	public Message()
	{


	private DateTime validFrom;
	private DateTime validTo;
	private String validUser;
	private int ReplayTime;
	private String MessageText;
		
	
	public Message(DateTime _validFrom, DateTime _validTo, String _validUser, String _ReplayTime, String _MessageText)
    {

		this.validFrom = _validFrom;
		this.validTo = _validTo;
		this.validUser = _validUser;
		this.ReplayTime = _ReplayTime;
		this.MessageText = _MessageText;


    }


	}
}
