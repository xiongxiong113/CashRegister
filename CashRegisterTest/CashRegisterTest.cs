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

		[Fact]
		public void Should_print_purchase_content_when_run_process_given_stub_purchase()
		{
            //given
            var spyPrinter = new Mock<Printer>();
            var cashRegister = new CashRegister(spyPrinter.Object);
            var stubPurchase = new Mock<Purchase>();
            stubPurchase.Setup(x => x.AsString()).Returns("moq stub purchase");
            //when
            cashRegister.Process(stubPurchase.Object);
			//then
			spyPrinter.Verify(_ => _.Print("moq stub purchase"));
        }

		[Fact]
		public void Should_throw_HardwareException_when_run_process_given_stub_printer()
		{
            //given
            var printer = new Mock<Printer>();
            var cashRegister = new CashRegister(printer.Object);
            var purchase = new Purchase();
			printer.Setup(_ => _.Print(It.IsAny<string>())).Throws(new PrinterOutOfPaperException());
            //when
			//then
			var exception = Assert.Throws<HardwareException>(() => cashRegister.Process(purchase));
			Assert.Equal("Printer is out of paper.", exception.Message);
        }
    }
}
