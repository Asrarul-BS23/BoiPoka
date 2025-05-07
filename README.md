# BoiPoka
**Online Book Seller**

https://github.com/user-attachments/assets/9f6fba4c-c65c-4b12-8b62-15998008e6cc

## Features
- **Authentication & Authorization (Role-based)**
- **Book Mangement for Admin**
- **Cart Management**
- **Order Management for Admin**
- **Checkout & Place Order**
- **Filter & Search Books**
- **Admin Dashboard & User Profile**

## Authentication
- Login/Signup with email & password​
- Password reset for registered users​
- Frontend validates inputs before submission​
- Email must be unique​
- Password must meet strength rules​
- Successful login redirects to Home page​
<img width="945" alt="Register" src="https://github.com/user-attachments/assets/4f9fe757-c237-4727-a670-8323b11a5297" />
<img width="956" alt="Login" src="https://github.com/user-attachments/assets/ebdf1d42-a6f3-43e8-96b5-f8f9b3698858" />
<img width="955" alt="Forget password1 " src="https://github.com/user-attachments/assets/5b556315-84d5-4798-9391-5af9da5cb02c" />
<img width="956" alt="Change pass" src="https://github.com/user-attachments/assets/880942a1-f1d1-4d82-bbba-e9ae3f7f8d07" />



## Book management
- Admin Adds/Edits/Deletes books​
- Books must have titles, price, cover image, stock quantity​
- Users can only view book details​
- Categories are admin-created and managed​
- Price must be a positive number​
- Book cover image by image upload​
<img width="944" alt="Book Management" src="https://github.com/user-attachments/assets/45a9fc90-3c82-48f9-8a8b-578bd707a3bb" />
<img width="948" alt="Create Book" src="https://github.com/user-attachments/assets/5c41fae5-9150-4f9b-a440-a75a34341e01" />
<img width="945" alt="Edit book" src="https://github.com/user-attachments/assets/3dcbc511-82fc-447a-9a37-75d90dbea67e" />
<img width="959" alt="Add new Category" src="https://github.com/user-attachments/assets/f8ba9e88-df86-420a-b062-63dfc03124f8" />

## Manage Cart
- Add or Remove cart items​
- Update item quantity (min 1)​
- See subtotal and grand total​
- Cart tied to logged-in user​
- No guest cart support yet​
- Stock limits must be respected​
<img width="953" alt="Cart Manager" src="https://github.com/user-attachments/assets/5e0ab17d-0973-41fe-b47f-23776a645eac" />

## Checkout & Place Order
- Navigate cart page from profile/ cart icon​
- Update quantity of the items if necessary​
- Checkout to buy the products​
- Give necessary information to deliver products​
- Place Order​
<img width="945" alt="Checkout page" src="https://github.com/user-attachments/assets/629b75c5-c648-4f37-b118-2dd013bc17ff" />

## Manage Orders
- View all orders from admin dashboard​
- Update order status (4 levels)​
- Only admin can change status​
- Final status can’t be changed again​
- No notifications or audit logging
<img width="941" alt="Order Management" src="https://github.com/user-attachments/assets/c776df79-9f5d-48f3-9816-eaf3eb9bd145" />

## Filter & Search Books
- Search books by title input​
- Filter books by category dropdown​
- Both filters can be combined​
- Search is case-insensitive​
- No sorting or suggestions implemented​
<img width="949" alt="Home page with filter" src="https://github.com/user-attachments/assets/aecb2bd0-e28c-4802-807d-2aaef5be81d3" />

## Admin Dashboard & User Profile
- Role-based redirect after login​
- Admin sees dashboard with controls​
- User sees personal profile page​
- Links to cart & order history​
- Only admins can access management tools​
- No personal info update support​
<img width="959" alt="Admin Dashboard" src="https://github.com/user-attachments/assets/56c3a54f-6061-46f3-b109-5db0fd76833e" />
<img width="952" alt="user profile" src="https://github.com/user-attachments/assets/b8569456-cb11-4db8-be54-48b90a011f5a" />
<img width="946" alt="Order history" src="https://github.com/user-attachments/assets/0ae81051-1c17-4488-a636-6b08b9445bbd" />

## Technology used:
<ul>
 <li>Backend : ASP.NET 9.0​</li>
 <li>Frontend: Razor and Bootstrap​​</li>
 <li>Server Side: SQL Server​​</li>
 <li>IDE: Visual Studio​​</li>
 <li>Deployment: Internet Information Service (IIS)​​</li>
</ul>
