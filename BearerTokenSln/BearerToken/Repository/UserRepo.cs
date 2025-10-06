using BearerToken.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;

namespace BearerToken.Repository
{
    //এটি IDisposable interface implement করেছে,
//মানে — যখন কাজ শেষ হয়ে যাবে, তখন Dispose() মেথড দিয়ে resources(যেমন database connection) release করা যাবে।
    public class UserRepo:IDisposable
    {
        //এখানে AppDbContext হচ্ছে তোমার Entity Framework database context ক্লাস।
//এটা ডাটাবেজের সাথে কানেকশন রাখে এবং model(যেমন Users, Orders etc.) map করে।
// private মানে — এটা শুধু এই ক্লাসের ভিতরেই ব্যবহার করা যাবে। readonly মানে — একবার তৈরি হয়ে গেলে পরে আর পরিবর্তন করা যাবে না।

//অর্থাৎ, db অবজেক্ট হলো ডাটাবেজে query চালানোর জন্য দরকারি কানেকশন।
        private readonly AppDbContext db=new AppDbContext();

        //Dispose() মেথড কল করে AppDbContext-এর কানেকশন ও resources মুক্ত করা হচ্ছে,
//যাতে memory leak বা অপ্রয়োজনীয় connection না থাকে।
        public void Dispose() {
        db.Dispose();
        }
        public User ValidateUser(string userName,string password)
        {
            //যদি username এবং password দুটোই মিলে যায় →তাহলে সেই user অবজেক্ট return হবে।যদি না মিলে →
            //তাহলে FirstOrDefault() null return করবে।
            return db.Users.FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && u.Password == password);
        }
    }
}