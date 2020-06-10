SELECT p.Name AS 'ProductName', c.Name AS 'CategoryName'
FROM Product p
LEFT JOIN ProductCategory pc ON p.ProductId = pc.ProductId
LEFT JOIN Category c ON pc.CategoryId = c.CategoryId
ORDER BY c.Name DESC
