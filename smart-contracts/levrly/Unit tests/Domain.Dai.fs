[<RequireQualifiedAccess>]
module Domain.Dai

open Infrastructure
open Contracts
open AbiTypeProvider.Common

let dollar n = bigint (decimal (10f ** 18f) * n)

let checkDaiBalance (ctx: TestContext) (dai: DAI) = async {
    return! dai.balanceOfQueryAsync ctx.Connection.Account.Address |> Async.AwaitTask
}

let grab (ctx: TestContext) (dai: DAI) amount = async {
    let callData = 
        dai.mintTransactionInput(
            usr = ctx.Connection.Account.Address, 
            wad = dollar amount,  
            // weiValue = weiValue 0UL,
            // gasLimit = gasLlimit 9500000UL,
            // gasPrice = gasPrice 0UL,
            From = "0xb5b06a16621616875A6C2637948bF98eA57c58fa",
            To = dai.Address)
    let! txr = ctx.Connection.MakeImpersonatedCallAsync callData
    
    if txr.Status <> ~~~ 1UL then
        failwith "Transaction not succeed"
    
    let events = DAI.TransferEventDTO.DecodeAllEvents(txr)
    if events.Length = 0 then
        failwith "No Trnasfer event in transaction"
}

let approveLendingPool (ctx: TestContext) (dai: DAI) amount = async {
    let dollar amount = decimal (10f ** 18f) * amount |> bigint
    let! txr = await ^ dai.approveAsync(configuration.Addresses.AaveLendingPool, dollar amount)
    if txr.Status <> ~~~ 1UL then
        failwith "Transaction not succeed"
    
    let events = DAI.ApprovalEventDTO.DecodeAllEvents(txr)
    if events.Length = 0 then
        failwith "No Trnasfer event in transaction"
}