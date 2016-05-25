using UnityEngine;
using System.Collections;

public class GameMessage  {
	private MessageTypeEnum messageType;
	private object payload;

	public GameMessage (MessageTypeEnum messageType, object payload)
	{
		this.messageType = messageType;
		this.payload = payload;
	}
	
	MessageTypeEnum MessageType {
		get {
			return this.messageType;
		}
		set {
			messageType = value;
		}
	}

	object Payload {
		get {
			return this.payload;
		}
		set {
			payload = value;
		}
	}
}
