var publisher = new ExtremeWeatherAlertPublisher();
var sms_subscriber = new SMSSubscriber();
var email_subscriber = new EmailSubscriber();

publisher.Subscribe(sms_subscriber);
publisher.Subscribe(email_subscriber);

publisher.AddNewAlert(new ExtremeWeatherAlert(DateTime.Now, "EXTREME HEAT WAVE"));


public class ExtremeWeatherAlert {
    public DateTime Date {get;}
	public string Description {get;}

    public ExtremeWeatherAlert(DateTime date, string description){
        Date = date;
        Description = description;
    }
    
}


public interface IExtremeWeatherAlertSubscriber {
	public void Update(ExtremeWeatherAlert alert); 	
}

public interface IExtremeWeatherAlertPublisher {
	public void Subscribe(IExtremeWeatherAlertSubscriber subscriber);
	public void Unsubscribe(IExtremeWeatherAlertSubscriber subscriber);
    public void AddNewAlert(ExtremeWeatherAlert alert);
}

public class ExtremeWeatherAlertPublisher : IExtremeWeatherAlertPublisher
{
    private List<IExtremeWeatherAlertSubscriber> _subscribers = new List<IExtremeWeatherAlertSubscriber>();

    public void AddNewAlert(ExtremeWeatherAlert alert)
    {
        foreach(var subscriber in _subscribers){
            subscriber.Update(alert);
        }
    }

    public void Subscribe(IExtremeWeatherAlertSubscriber subscriber)
    {
        _subscribers.Add(subscriber);
    }

    public void Unsubscribe(IExtremeWeatherAlertSubscriber subscriber)
    {
        _subscribers.Remove(subscriber);
    }
}

public class SMSSubscriber : IExtremeWeatherAlertSubscriber
{
    public void Update(ExtremeWeatherAlert alert)
    {
        Console.WriteLine($"Sending out SMS with the alert: {alert.Description} on {alert.Date}");
    }
}

public class EmailSubscriber : IExtremeWeatherAlertSubscriber
{
    public void Update(ExtremeWeatherAlert alert)
    {
        Console.WriteLine($"Sending out Email with the alert: {alert.Description} on {alert.Date}");
    }
}

