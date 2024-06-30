select * from users
select * from menus
select * from carts
select * from cartDetails
select * from admins
select * from customers
select * from orders
select * from OrderDetails
select * from feedbacks
select * from FbComments
select * from Payments

drop database FoodDeliveryAppDB;

delete from carts;
delete from orders;
delete from OrderDetails;
delete from feedbacks
delete from FbComments
delete from users where id!=1
delete from customers



ALTER TABLE dbo.Payments
DROP CONSTRAINT FK_Payments_Orders_CustomerId;

ALTER TABLE dbo.Payments
ADD CONSTRAINT FK_Payments_Orders_CustomerId
FOREIGN KEY (CustomerId) REFERENCES Customers (Id);

ALTER TABLE dbo.Feedbacks
add CONSTRAINT FK_Feedbacks_FbComments_CommentId
FOREIGN KEY (CommentId) REFERENCES FbComments (CommentId);

ALTER TABLE dbo.Feedbacks
add CONSTRAINT FK_Feedbacks_Menus_CommentId
FOREIGN KEY (fId) REFERENCES Menus (fId);

ALTER TABLE dbo.Feedbacks
ADD CONSTRAINT FK_Feedbacks_FbComments_CommentId
FOREIGN KEY (CommentId) REFERENCES dbo.FbComments(CommentId) ON DELETE CASCADE;
