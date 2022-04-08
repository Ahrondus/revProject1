create table MenuOptions
(
	pId int primary key identity,
	pName varchar(30) not null,
	pPrice float(2) not null,
	pCategory varchar(20) not null,
	pQuantity int
);
create table Customers
(
	cId int primary key identity,
	cName varchar(40) not null,
	cNumofOrders int,
	cBill float(2)
);
create table Orders 
(
	oId int primary key identity,
	oCId int not null,
	oTotal float(2),

	foreign key(oCId) references Customers(cId)
);
create table OrderItems
(
	oItemId int identity,
	oRefId int not null,
	pRefId int not null,

	foreign key(oRefId) references Orders(oId),
	foreign key(pRefId) references MenuOptions(pId)
);

select * from MenuOptions
select * from Customers
select * from Orders
select * from OrderItems

create procedure prc_Sale
(
	@productId int,
	@orderId int
)
as
begin
	declare @price float(2) = (select pPrice from MenuOptions where pId = @productId)

begin transaction
	update Orders set oTotal = oTotal + @price where oId = @orderId;
	update MenuOptions set pQuantity = (pQuantity-1) where pId = @productId;
commit
end

create trigger tgr_addOrdertoCustomer
on Orders
after insert
as
begin
	declare @trID int = (select i.oCId from inserted i)
	declare @trTot float(2) = (select i.oTotal from inserted i)
begin transaction
	update Customers set cNumofOrders = (cNumofOrders+1) where cId = @trID;
	update Customers set cBill = @trTot;
commit
end