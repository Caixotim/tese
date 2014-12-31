// WiimoteReceiver.cs created with MonoDevelop
// User: jzs at 19:08 04/15/2009
//
// To change standard headers go to Edit->Preferences->Coding->Standard Headers
//
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using OSC.NET;

public class WiimoteReceiver {
	
	private bool connected = false;
	private int port = 8876;
	private OSCReceiver receiver;
	private Thread thread;
	public Dictionary<int,Wiimote> wiimotes = new Dictionary<int, Wiimote>();
	
	public WiimoteReceiver() {}
	
	public WiimoteReceiver(int port)
	{
		this.port = port;
	}
	
	public int getPort()
	{
		return port;
	}
	
	
	// Use this for initialization
	public void connect() {
		try 
		{
			receiver = new OSCReceiver(port);
			thread = new Thread(new ThreadStart(listen));
			thread.Start();
			connected = true;
		} 
		catch(Exception e)
		{
			Console.WriteLine("Failed to connect to port" + port);
			Console.WriteLine(e.Message);
		}
		//oscReceiver.Connect();
	}
	
	public void disconnect()
	{
		if(receiver != null) receiver.Close();
		receiver = null;
		connected = false;
	}
	
	public bool isConnected() { return connected; }
	
	private void listen()
	{
		while(connected)
		{
			try
			{
				OSCPacket packet = receiver.Receive();
				if(packet != null)
				{
					if (packet.IsBundle()) {
						ArrayList messages = packet.Values;
						for (int i=0; i<messages.Count; i++) {
							processMessage((OSCMessage)messages[i]);
						}
					} else processMessage((OSCMessage)packet);		
				} else Console.WriteLine("Null packet");
			} catch (Exception e) { Console.WriteLine(e.Message); }
		}
	}
	
	private void processMessage(OSCMessage message)
	{
		
		string address = message.Address;
		int wiimoteID = int.Parse(address.Substring(5,1));
		// Is wii OSC message
		if( String.Compare(address.Substring(1,3), "wii") == 0)
		{
			// Is wiimote id between 1 and 4
			if( wiimoteID >= 1 && wiimoteID <= 4)
			{
				// Does Wiimote object id already exist ?
				if( !wiimotes.ContainsKey(wiimoteID) )
				{
					wiimotes.Add(wiimoteID, new Wiimote(wiimoteID) );
				}
				// Update Wiimote Object
				try
				{
					Wiimote mote = wiimotes[wiimoteID];
					string wiiEvent = address.Substring(7);
					mote.update(wiiEvent, ArrayList.ReadOnly(message.Values), DateTime.Now );
				} catch(Exception e) 
				{
					Console.WriteLine( "Failed to get Wiimote Object from Dictionary" + e.Message);
				}
			}
		}
	}
}