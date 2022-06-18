
namespace OrderCLI
{
    // Interfaces of the program can be used both product or order validation or verification
    // Since there is no option to add product interfaces hasn't been implemented
    
    public interface IValidation // To validate if the given input by user is valid such as identity number lenght
    {
        public bool ValidateInput();
    }
    public interface IVerification <T>// To verificate if the given input by user is unique such as identity number
    {
        public bool VerifyInput(List<T> list , String input);
    }

    public class User : IValidation , IVerification
    {
        // To define features of a user
        public string username { get; set; }
        public string password { get; set; }
        public string identityNumber { get; set; }
        public string userId { get; set; }
        public User(String identityNumber, String password)
        {
            
            this.identityNumber = identityNumber;
            this.password = password;

        }
   
        public bool ValidateInput()
        {
            // To check if the both identity number and password input is valid
            if (this.identityNumber.Length == 11 && this.password.Length >= 6) 
            {
                Console.WriteLine("Valid Input\n");
                return true;
               
            }
            else
            {
                Console.WriteLine("Unvalid Input\n Identity Number Must be 11 chracter \n Password must be at least 6 chracter\n");
                return false;
            }
         
        }

        public bool VerifyInput(List<User> UserList , String input)
        {
            // To check if the  identity number input is unique in the user list
            bool uniquenessCounter = true;
                foreach (User user in UserList)   // To check if the given identity number matches with any record of user list
                {
                    if (input == user.identityNumber) // Matched
                    {
                    Console.WriteLine("Identity number must be Unique\n");
                    uniquenessCounter = false;
                    break;
          
                    }
                    else // Not matched
                    {
                    uniquenessCounter = true;
                     
                    
                    }
                }
            return uniquenessCounter;
        }

    
    }
  
    public class Product 
    {
        // To define features of a product
        public int productId { get; set; }
        public string productName { get; set; }
        public int productQuantity { get; set; }
        public int productPrice { get; set; }
        public Product(int productId , String productName , int productQuantity , int productPrice)
        {
            this.productName = productName;
            this.productId = productId; 
            this.productQuantity = productQuantity; 
            this.productPrice = productPrice;
        }

    }
    public class Order 
    {
        public int orderId { get; set; }
        public int orderPrice { get; set; }
        public string orderedProduct { get; set; }
        public Order( Product Product , int quantity) // Order takes a product in its constructor to create an order
        {
            this.orderedProduct = Product.productName;
            this.orderPrice = Product.productPrice * quantity;
        }
    }
   
    class Program
    {

        static void Main(string[] args)
        {
            bool applicationCounter = true; // To keep application work if true or to finish if false 
            string applicationRedirecter = ""; // To redirect application logic
            var UserList = new List<User>();  //To Keep the list of Users
            var ProductList = new List<Product>();  //To Keep the list of Products
            var OrderList = new List<Order>();  //To Keep the list of Orders 

            UserList.Add(new User("11111111111","111111")); //Pre - Defined User
            ProductList.Add(new Product(1,"Phone",500,5000)); //Pre - Defined User
            ProductList.Add(new Product(2, "Laptop", 500, 10000)); //Pre - Defined User
            ProductList.Add(new Product(3, "Camera", 500, 3000)); //Pre - Defined User

            while (applicationCounter)
            {
                Console.WriteLine("Please Enter Your Identity Number : "); //To Redirect User
                string identityNumberInput; // To hold given identity number
                string passwordInput; // To hold given password

                identityNumberInput = Console.ReadLine(); //To get identity number Input from the user
                Console.WriteLine("\n");
                foreach (User user in UserList)   // To check if the given identity number matches with any record of user list
                {
                    if (identityNumberInput == user.identityNumber) // Matched
                    {
                        Console.WriteLine("User has been found in the UserList\n");
                        applicationRedirecter = "Login"; // To redirect user to Login Phase
          
                    }
                    else // Not matched
                    {
                        Console.WriteLine("User hasn't been found in the UserList Please Register");
                        applicationRedirecter = "Register"; // To redirect user to Register Phase
                     
                    
                    }
                }

                switch (applicationRedirecter)
                {
                    case "Login": 
                        Console.WriteLine("Login Phase "); //Login
                        Console.WriteLine("Please Enter Your Password ");
                        passwordInput = Console.ReadLine(); // To get users password Input
                        Console.WriteLine("\n");
                        foreach (User users in UserList)   // To check if the given identity number and password matches with any record of user list
                        {
                            if (identityNumberInput == users.identityNumber && passwordInput == users.password) // Matched
                            {
                                Console.WriteLine("Login Operation Completed Succesfully"); // Login Successfull      
                                Console.WriteLine("Logined User Information \n Identity Number :  " + users.identityNumber+"\n");
                                break; 
                            }
                            else // Not matched
                            {   
                                Console.WriteLine("Invalid credentials"); // Invalid credentials
                                goto case "Login";
                            }
                        }
                        applicationRedirecter = "MainPage";
                        break;
                    case "Register": // Register
                        Console.WriteLine("Register Phase ");
                        Console.WriteLine("Please Enter Your IdentityNumber "); //To get users identity Input
                        identityNumberInput = Console.ReadLine();
                        Console.WriteLine("\n");
                        Console.WriteLine("Please Enter Your Password ");// To get users password Input
                        passwordInput = Console.ReadLine();
                        Console.WriteLine("\n");
                        User user = new User(identityNumberInput,passwordInput);
                        if (user.ValidateInput() && user.VerifyInput(UserList,identityNumberInput)) // To validate and veriface input
                        {
                            UserList.Add(user); // to add user into user list if validated and verificated
                            Console.WriteLine("---Logined User Information--- \nIdentity Number : "+user.identityNumber);
                            applicationRedirecter = "MainPage";
                            break;
                        }
                        else
                        {     
                            goto case "Register";
                        }
                      

                    default:
                        break;

                }

                while (applicationRedirecter != "3") // Until the exit selected program rybs
                {

                    Console.WriteLine("Please select operation"); 
                    Console.WriteLine("1 - Buy Product ");
                    Console.WriteLine("2 - Order List ");
                    Console.WriteLine("3 - Exit ");
                    applicationRedirecter = Console.ReadLine(); // To redirect program
                    Console.WriteLine("\n");
                    switch (applicationRedirecter)
                    {
                        case "1":
                            Console.WriteLine("Product ID --- Product Name --- Product Price --- Product Quantity");
                            foreach (Product product in ProductList)   // To print product list
                            {
                                Console.WriteLine(product.productId + " - " + product.productName + " - " + product.productPrice + " - " + product.productQuantity);
                            }
                            Console.WriteLine("Please Enter Product ID That You Want To Buy"); //To Get desired order input
                            String message="";
                            String selectedProduct = Console.ReadLine();
                            Console.WriteLine("\n");
                            Console.WriteLine("Please Enter Quantity");
                            String selectedProductQuantity = Console.ReadLine();
                            Console.WriteLine("\n");


                            foreach (Product selected in ProductList)   
                            {
                               if(selected.productId == Int32.Parse(selectedProduct)) // To check if the user's given product id in product list
                                {
                                    if(selected.productQuantity < Int32.Parse(selectedProductQuantity)) // To check if there is enough product

                                    {
                                        message = "There is not enough product to buy";
                                        break;
                                    }else
                                    {
                                        // If Enogh product and given product id matches in any product in product list
                                        // Creating a new order and adding it into order list
                                        selected.productQuantity = selected.productQuantity - Int32.Parse(selectedProductQuantity);
                                        //To decrease order amount of quantity in products quantity
                                        Order order = new Order(selected, Int32.Parse(selectedProductQuantity));
                                        OrderList.Add(order);
                                        message = "Order Has Been Recieved";    
                                        break;
                                    }
                                    
                                }else
                                {
                                    message = "Please Enter Valid product id";
                                }
                            }
                            Console.WriteLine(message);
                            break;
                        case "2":
                            Console.WriteLine("Ordered Product - Order Price");
                            foreach (Order order in OrderList)   // To print order list
                            {
                                Console.WriteLine(order.orderedProduct + " - " + order.orderPrice +"\n");
                            }

                            break;
                        case "3":
                            applicationRedirecter = "3"; // To Exit
                            applicationCounter = false;
                            break;

                        default:
                            Console.WriteLine("Invalid Operation"); // If given operation doesn't match menu options
                            break;
                    }

                }



            }
        }
    }
}