using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericsBeispiel {
    class Program {
        static void Main ( string [] args ) {
            GenerischeKlasse<int> gk1 = new GenerischeKlasse<int> ();
            gk1.Add ( 42 );
            Console.WriteLine ( gk1.Show () );

            GenerischeKlasse<string> gk2 = new GenerischeKlasse<string> ();
            gk2.Add ( "Zweiundvierzig" );
            Console.WriteLine ( gk2.Show () );

            // ************************ Constraints im Hauptprogramm ''''''''''''''''''''''''
            Console.WriteLine ("to be a constraint or not to be a constraint that is the question ...");
            
            Person p = new Person () { IQ = 0 };  // Typ-kompatibel
            Person p2 = new Person () { IQ = 0 };  // Typ-kompatibel
            p2.Lernen (); // einmal Lernen
            p2.Lernen (); // zweimal Lernen
            p2.Lernen (); // dreimal Lernen

            UnPerson up = new UnPerson () { IQ = 1 }; // nicht Typ-kompatibel

            ConstraintsKlasse<Person> cc_ok = new ConstraintsKlasse<Person>();

            // geht nicht, weil der Typ über die Constraints geregelt, nicht kompatibel ist
            // und das, obwohl in der Klasse UnPerson ein und dasselbe steht wie in Person!!!
            // zum Gucken: Einkommentieren !!!
            //ConstraintsKlasse<UnPerson> cc_nixOk = new ConstraintsKlasse<UnPerson> ();

            cc_ok.Add ( p ); // klappt !!!                             
            cc_ok.Add ( p2);

            // Wichtig !!!
            //cc_ok.Add ( up ); // klappt nicht !!
            
            cc_ok.Show ();
            

            Console.ReadLine ();
        }
    }

    public class GenerischeKlasse<T> {
        private T [] elements = new T [ 2 ];

        public void Add ( T t ) {
            elements [ 0 ] = t;
        }

        public T Show () {
            return elements [ 0 ];
        }
    }
    // ************************ Constraints ***********************

    interface IBitLC {
        int IQ { get; set; }
        void Lernen ();
    }

    class ConstraintsKlasse<T> where T : IBitLC {
        private List<T> liste = new List<T> ();

        public void Add ( T element ) {
            liste.Add ( element );
        }

        public void Show () {
            foreach ( var item in liste ) {
                Console.WriteLine ( item.IQ );
            }
        }
    }

    public class Person : IBitLC {   // Erbt von IBitLC
        public int IQ { get; set; }

        public void Lernen () {
            this.IQ += 50;
        }
    }

    public class UnPerson { // keine Vererbung vorgesehen !!!
        public int IQ { get; set; }

        public void Lernen () {
            this.IQ += 50;
        }
    }
}
