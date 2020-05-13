Select p.Name as 'ProductName', c.Name as 'CategoryName'
From Product p
Left Join ProductCategory pc On p.ProductId = pc.ProductId
Left Join Category c On pc.CategoryId = c.CategoryId
Order by c.Name desc
