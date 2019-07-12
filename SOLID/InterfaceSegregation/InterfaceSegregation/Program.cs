using System;
using System.Reflection.Metadata;

namespace InterfaceSegregation
{
    public interface IPrinter
    {
        void Print(Document d);
    }

    public interface IScanner
    {
        void Scan(Document d);
    }

    public interface IMultiFunctionMachine : IPrinter, IScanner
    {

    }

    public class CannonXS430 : IMultiFunctionMachine
    {
        private IPrinter printer;
        private IScanner scanner;

        public CannonXS430(IPrinter printer, IScanner scanner)
        {
            this.printer = printer;
            this.scanner = scanner;
        }

        //delegate implementation
        public void Print(Document d)
        {
            printer.Print(d);
        }

        public void Scan(Document d)
        {
            scanner.Scan(d);
        } //decorator pattern
    }

    public class OldStylePrinter : IPrinter
    {
        public void Print(Document d)
        {
            //
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }

    public class Document
    {

    }
}
