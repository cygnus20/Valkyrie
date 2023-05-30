namespace Valkyrie.Entities;

public static class SeedData
{
    public static void AddDevboards(WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetService<ValkDbContext>();

        if (!db!.DevBoards.Any())
        {
            db?.DevBoards.AddRange(
            new DevBoard
            {
                Name = "Arduino Uno R3",
                Description = "Arduino UNO is a microcontroller board based on the ATmega328P. " +
                               "It has 14 digital input/output pins (of which 6 can be used as PWM outputs), 6 analog inputs, a 16 MHz ceramic resonator, a USB connection, " +
                               "a power jack, an ICSP header and a reset button. It contains everything needed to support the microcontroller; " +
                               "simply connect it to a computer with a USB cable or power it with a AC-to-DC adapter or battery to get started. " +
                               "You can tinker with your UNO without worrying too much about doing something wrong, " +
                               "worst case scenario you can replace the chip for a few dollars and start over again.",
                Platform = "Arduino",
                Family = "Classic",
                MainMCU = new MCU
                {
                    Name = "ATmega328P",
                    Architecture = "AVR",
                    Family = "ATmega",
                    Frequency = 8000000,
                    Memories = new List<Memory>
                    {
                        new Memory { Type = "SRAM", Size = 2000 },
                        new Memory { Type = "FLASH", Size = 3200 },
                        new Memory { Type = "EEPROM", Size = 1000 }
                    }
                },
                Pins = new Pins
                {
                    DigitalIO = 14,
                    AnalogInput = 6,
                    PWM = 6,
                    PWMPins = new List<string>
                    {
                        "D3", "D5", "D6", "D9", "D10", "D11"
                    },
                    BuiltinLEDPins = new List<string>
                    {
                        "D5"
                    }
                },
                Communications = new Communications
                {
                    UART = new List<UART>
                    {
                        new UART { RX = "D0", TX = "D1" }
                    },
                    I2C = new List<I2C>
                    {
                        new I2C { SDA = "D18", SCL = "D19" }
                    },
                    SPI = new List<SPI>
                    {
                        new SPI { SS = "D10", COPI = "D11", CIPO = "D12", SCK = "D13" }
                    }
                },
                Dimensions = new Dimensions
                {
                    Weight = 25,
                    Width = 53.4f,
                    Length = 68.6f,
                }
            });

            db?.SaveChanges();
        }

        if (!db!.SBC.Any())
        {
            db?.SBC.AddRange(
                new SBC
                {

                }
                );
        }

    }
}
