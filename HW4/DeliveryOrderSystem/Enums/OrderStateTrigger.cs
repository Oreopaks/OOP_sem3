namespace DeliveryOrderSystem.Enums
{
    public enum OrderStateTrigger 
    { 
        StartPreparation, 
        ReadyForPickup, 
        AssignCourier, 
        Deliver, 
        Complete, 
        Cancel 
    }
}