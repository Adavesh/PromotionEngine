# Promotion Engine
<br>
This promotion engine applies a selected promotion on a cart.

## Models
<strong>Product:</strong> Represents a product that can be added to cart<br>
<strong>Cart:</strong> Represents a shopping cart which can contain one or more products<br>
<strong>CartItem:</strong> Represents a single item in cart with quantity<br>
<strong>Promotion:</strong> Represents a promotion which contains one or more PromotionItems<br>
<strong>PromotionItem:</strong> Represents promotion item and quantity to be considered for the promotion. E.g 3 A's, 2 B's etc.<br>

##Services
<strong>IProductsService:</strong> Service for creating, updating, deleting and fetching Product instances.<br>
<strong>IPromotionsService:</strong> Service  creating, updating, deleting and fetching Promotion instances <br>
<strong>IDiscountProvider:</strong> Service for determining the discount on cart for given Promotion<br>

##Service Implementations
<strong>InMemoryProducts:</strong> InMemory implementation of IProductsService.<br>
<strong>InMemoryPromotions:</strong> InMemory implementation of IPromotionsService.<br>
<strong>FixedPriceDiscountProvider:</strong>Implementation of IDiscountProvider. Applies fixed discount value on cart. E.g 3 A's for 130. This uses in memory discount schemes.<br>
<strong>InMemoryProducts:</strong>Implementation of IDiscountProvider. Not implemented yet.<br>

##CartManager
Manages the CRUD operations on cart and also applies promotions on cart.

