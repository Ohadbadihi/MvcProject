using Microsoft.EntityFrameworkCore;
using MyProject.Models;

namespace MyProject.Data
{
    public class PetShopContext : DbContext // init here 
    {
        public PetShopContext(DbContextOptions<PetShopContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<CommentModel> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) // לאתחל את הטבלאות עם מידע 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CommentModel>() // set the relasionship menually
                 .HasOne(comment => comment.Animal)
                 .WithMany(animal => animal.Comments)
                 .HasForeignKey(comment => comment.AnimalId)
                 .OnDelete(DeleteBehavior.Cascade);


            //Insert Categories
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, CategoryName = "Mammals" },
                new Category { CategoryId = 2, CategoryName = "Birds" },
                new Category { CategoryId = 3, CategoryName = "Reptiles" },
                new Category { CategoryId = 4, CategoryName = "Insects" },
                new Category { CategoryId = 5, CategoryName = "Fish" }
            );

            //Insert Animals
            modelBuilder.Entity<Animal>().HasData(
                new Animal { AnimalId = 1, Name = "golden hamster", Age = 1, PictureName = "Images\\depositphotos_375487264-stock-photo-cute-hamster-syrian-hamster-stone.jpg", Description = "The golden hamster, also known as the Syrian hamster, is a small rodent and a popular pet. Native to Syria, this hamster has a golden-brown coat with a distinctive white belly. Known for their friendly and curious nature, golden hamsters prefer to live alone as they are solitary animals. They are especially active in the evening and enjoy exploring tunnels, wheels, and chew toys. Golden hamsters are nocturnal, meaning they are most active at night and sleep during the day. They require a well-ventilated cage with bedding, an exercise wheel, and a balanced diet to stay healthy.", CategoryId = 1 },

                new Animal { AnimalId = 2, Name = "Eagle", Age = 3, PictureName = "Images\\709a1422ea16a18402aa1b3df5f1c501.jpg", Description = "An eagle is a bird of prey and a member of the family Accipitridae. Known for their keen eyesight and powerful talons, eagles are formidable hunters. They inhabit various environments, including mountains, forests, and coastal regions. Eagles build large nests, called eyries, and are known for their strong, soaring flight. They care for their young by feeding them and protecting them until they are ready to fledge.", CategoryId = 2 },

                new Animal { AnimalId = 3, Name = "Ball Python", Age = 7, PictureName = "Images\\bc4cad3a0085c048b749b3808b65d1ce7ed459f7.jpeg", Description = "The ball python, or royal python, is a non-venomous constrictor snake from Africa. Known for their calm nature and manageable size, they are popular pets among reptile enthusiasts. Their distinctive dark brown or black markings vary among individuals. Named for their defensive behavior of curling into a ball when threatened, ball pythons need a well-maintained terrarium with appropriate heat, humidity, hiding spots, and a water dish. They primarily eat small mammals like mice and rats. With proper care, they can live over 20 years in captivity.", CategoryId = 3 },



                new Animal { AnimalId = 4, Name = "Orchid Mantis", Age = 4, PictureName = "Images\\Mantis_HERO.jpg", Description = "The orchid mantis is a striking insect and a member of the mantid family, known for its remarkable camouflage. Native to Southeast Asia, this mantis mimics the appearance of an orchid flower, with its body and legs resembling the petals of a bloom. This adaptation helps it blend into its environment and ambush prey, such as other insects. The orchid mantis is an arboreal species, spending most of its time on plants and trees. It is prized among insect enthusiasts for its unique and beautiful appearance. In captivity, it requires a habitat with plenty of vertical space and live food to thrive.", CategoryId = 4 },


                new Animal { AnimalId = 5, Name = "Goldfish", Age = 1, PictureName = "Images\\goldfish.jpg", Description = "The goldfish is a popular freshwater aquarium fish native to East Asia. Known for their bright orange-golden color, they can also come in shades of white, black, and red. While typically small, they can grow larger in spacious environments. Goldfish are social and active swimmers, requiring a well-maintained aquarium with proper filtration and oxygenation. They are omnivorous, needing a balanced diet of high-quality fish food along with occasional live or frozen treats. Goldfish are hardy and can live for over 10 years with proper care. They thrive in cool water conditions and benefit from regular tank maintenance.", CategoryId = 5 }
              
            );

            modelBuilder.Entity<CommentModel>().HasData(

                new CommentModel { CommentId = 1, AnimalId = 1, Comment = "So cute!!!" },
                new CommentModel { CommentId = 2, AnimalId = 1, Comment = "Cutest thing ever!" },
                new CommentModel { CommentId = 3, AnimalId = 1, Comment = "Look at him he is so tiny!" },
                new CommentModel { CommentId = 4, AnimalId = 1, Comment = "I must to own one" },
                new CommentModel { CommentId = 6, AnimalId = 3, Comment = "Curious" },
                new CommentModel { CommentId = 7, AnimalId = 4, Comment = "So unique and fascinating!" },
                new CommentModel { CommentId = 8, AnimalId = 4, Comment = "Wow, just wow!" },
                new CommentModel { CommentId = 9, AnimalId = 4, Comment = "I never saw something like that, so beautiful." },
                new CommentModel { CommentId = 10, AnimalId = 4, Comment = "A true marvel of nature!" },
                new CommentModel { CommentId = 11, AnimalId = 4, Comment = "This mantis is a testament to the complexity of nature." },
                new CommentModel { CommentId = 12, AnimalId = 5, Comment = "The Goldfish displays a gentle and captivating beauty." },
                new CommentModel { CommentId = 13, AnimalId = 5, Comment = "A classic choice that's always good." },
                new CommentModel { CommentId = 14, AnimalId = 5, Comment = "Simple and convenient choice." }
                );

        }

    }
}
