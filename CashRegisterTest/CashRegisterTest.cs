namespace CashRegisterTest
{
	using CashRegister;
	using Moq;
	using Xunit;

	public class CashRegisterTest
	{
		[Fact]
		public void Should_process_execute_printing()
		{
			//given
			SpyPrinter printer = new SpyPrinter();
			var cashRegister = new CashRegister(printer);
			var purchase = new Purchase();
			//when
			cashRegister.Process(purchase);
			//then
			//verify that cashRegister.process will trigger print
			Assert.True(printer.HasPrinted);
		}

		[Fact]
		public void Should_print_called_When_Process_Given_spy_printer_create()
		{
			//given
			var printer = new Mock<Printer>();
            var cashRegister = new CashRegister(printer.Object);
            var purchase = new Purchase();
            //when
            cashRegister.Process(purchase);
			//then
			printer.Verify(_ => _.Print(It.IsAny<string>()));
        }
    }
}
