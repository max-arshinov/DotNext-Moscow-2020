// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open HightechAngular.Orders.Entities

type New = {
    OrderItems: OrderItem[]   
}

type Paid = {
    Total: decimal    
}

type Shipped = {
    TrackingCode: Guid    
}

type Order =
    | New of New
    | Paid of Paid
    | Shipped of Shipped
    

[<EntryPoint>]
let main argv =
    let order: Order = Paid({Total = decimal 100500})
    let str =
        match order with
        | New n -> "new"
        | Paid p -> "paid: " + p.Total.ToString()
        | Shipped s -> "shipped: " + s.TrackingCode.ToString()
    
    Console.WriteLine(str);    
    0 // return an integer exit code