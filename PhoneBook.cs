//BenjaminKramer
//12-9-2020

//A C# Phone Book that
//allows you to add new
//contacts in the form of
//a name and number
using System;
using System.Collections.Generic;

//Initializing the Phone Book
//Names and numbers (as Contacts)
class PhoneBook {
    List<Contact> numbers;
    //Making the contact list
    public PhoneBook() {
        numbers = new List<Contact>();
    }
    //Adding a contact
    public bool add(string name, string number) {
        Contact nmbr = new Contact(name, number);
        Contact result = find(name);

        if (result == null) {
            numbers.Add(nmbr);
            return true;
        }
        else {
            return false;
        }
    }
    //Removing a contact
    public bool remove(string name) {
        Contact nmbr = find(name);

        if (nmbr != null) {
            numbers.Remove(nmbr);
            return true;
        }
        else {
            return false;
        }
    }
    //Assigning a number to each of the names
    public void list(Action<Contact> action) {
        numbers.ForEach(action);
    }
    //When the Phone Book
    //is void of contacts
    public bool isEmpty() {
        return (numbers.Count == 0);
    }
    //Allowing access to the
    //contacts via their name
    public Contact find(string name) {
        Contact nmbr = numbers.Find(
          delegate (Contact c) {
              return c.name == name;
          }
        );
        return nmbr;
    }
}
//Making the Contact object
class Contact {
    public string name;
    public string number;
    //Assigning the name and number
    //to create each contact
    public Contact(string name, string number) {
        this.name = name;
        this.number = number;
    }
}
//Initializing the "book" or
//collection of contacts
class PhoneBookPrompt {
    PhoneBook book;
    //Making a new book
    //for every instance
    public PhoneBookPrompt() {
        book = new PhoneBook();
    }
    //main
    static void Main(string[] args) {
        string selection = "";
        PhoneBookPrompt prompt = new PhoneBookPrompt();

        prompt.displayMenu();
        while (!selection.ToUpper().Equals("Q")) {
            Console.WriteLine("Choice: ");
            selection = Console.ReadLine();
            prompt.performAction(selection);
        }
    }
    //The choices to be displayed
    //when the program is run
    void displayMenu() {
        Console.WriteLine("Welcome Back To Your Phone Book");
        Console.WriteLine("=================================");
        Console.WriteLine("\tA - Add a Contact");
        Console.WriteLine("\tD - Delete a Contact");
        Console.WriteLine("\tE - Edit a Contact");
        Console.WriteLine("\tL - List All Contacts");
        Console.WriteLine("\tQ - Quit");
    }
    //Stating how each of the
    //choices affects the phone book
    //and the manipulation of the contacts
    void performAction(string selection) {
        string name = "";
        string number = "";
        //The following will be the
        //different cases and their 
        //procedures depending on the choice
        //the user makes with their input

        //As a note for exception-handling,
        //the selection.ToUpper() will
        //make it so that if a lower-case
        //choice was made, it is changed to
        //the required upper-case
        switch (selection.ToUpper()) {
            case "A":
                Console.WriteLine("\nEnter the Name for the Contact: ");
                name = Console.ReadLine();
                Console.WriteLine("Enter a Number for the Contact: ");
                number = Console.ReadLine();
                if (book.add(name, number)) {
                    Console.WriteLine("The name and number have been successfully added!\n");
                }
                else {
                    Console.WriteLine("It seems a number is already on file for {0}.", name, "\n");
                }
                break;
            case "D":
                Console.WriteLine("\nEnter a Name to Delete the Contact: ");
                name = Console.ReadLine();
                if (book.remove(name)) {
                    Console.WriteLine("The contact has been successfully removed\n");
                }
                else {
                    Console.WriteLine("The number for {0} could not be found.", name, "\n");
                }
                break;
            case "L":
                if (book.isEmpty()) {
                    Console.WriteLine("Sorry, there appears to be a lack of contacts.\n");
                }
                else {
                    Console.WriteLine("Your Contacts:\n");
                    book.list(
                      delegate (Contact c) {
                          Console.WriteLine("{0} - {1}", c.name, c.number);
                      }
                    );
                }
                break;
            case "E":
                Console.WriteLine("Enter a Name to Edit The Contact: ");
                name = Console.ReadLine();
                Contact nmbr = book.find(name);
                if (nmbr == null) {
                    Console.WriteLine("The number for {0} could not be found.", name,"\n");
                }
                else {
                    Console.WriteLine("Please enter a new Number: ");
                    nmbr.number = Console.ReadLine();
                    Console.WriteLine("The number has been updated for {0}", name, "\n");
                }
                break;
            }
        }
    }
