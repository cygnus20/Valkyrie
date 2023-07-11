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
                        new SPI { CE = "D10", COPI = "D11", CIPO = "D12", SCK = "D13" }
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
                    Name = "Raspberry PI 4 Model B",
                    Description = "Your tiny, dual-display, desktop computer and robot brains, smart home hub, media centre, networked AI core, factory controller, and much more",
                    Platform = "Raspberry PI",
                    OperatingSystems = new List<string>
                    {
                        "Raspberry PI OS", "Volumio", "RetroPie", "LibreELEC", "OpenMediaVault"
                    },
                    Power = new Power
                    {
                        IOVoltage = 3.3f,
                        IOPinCurrent = .016f,
                        InputVoltage = new InputVoltage
                        {
                            Min = 4.7f,
                            Max = 5.25f
                        }
                    },
                    Pins = new Pins
                    {
                        DigitalIO = 28,
                        PWM = 4,
                        PWMPins = new List<string> { "GPIO12 - Pin32", "GPIO13 - Pin33", "GPIO18 - Pin12", "GPIO19 - Pin35" },
                        
                    },
                    Communications = new Communications
                    {
                        UART = new List<UART>
                        {
                            new UART { TX = "GPIO14 - Pin8", RX = "GPIO15 - Pin10"  },
                            new UART { TX = "GPIO0 - Pin27", RX = "GPIO1 - Pin28"  },
                            new UART { TX = "GPIO4 - Pin7", RX = "GPIO5 - Pin29"  },
                            new UART { TX = "GPIO8 - Pin24", RX = "GPIO9 - Pin21"  },
                            new UART { TX = "GPIO12 - Pin32", RX = "GPIO13 - Pin33"  }
                        },
                        I2C = new List<I2C>
                        {
                            new I2C { SDA = "GPIO0 - Pin27", SCL = "GPIO1 - Pin28" },
                            new I2C { SDA = "GPIO2 - Pin3", SCL = "GPIO3 - Pin5" },
                            new I2C { SDA = "GPIO4 - Pin7", SCL = "GPIO5 - Pin29" },
                            new I2C { SDA = "GPIO6 - Pin31", SCL = "GPIO7 - Pin26" },
                            new I2C { SDA = "GPIO8 - Pin24", SCL = "GPIO9 - Pin21" },
                            new I2C { SDA = "GPIO10 - Pin19", SCL = "GPIO11 - Pin23" },
                            new I2C { SDA = "GPIO22 - Pin15", SCL = "GPIO23 - Pin16" }
                        },
                        SPI = new List<SPI>
                        {
                            new SPI { CIPO = "GPIO9 - Pin21", COPI = "GPIO10 - Pin19", SCK = "GPIO11 - Pin23", CE = "GPIO8 - Pin24 | GPIO7 - Pin26" },
                            new SPI { CIPO = "GPIO19 - Pin35", COPI = "GPIO20 - Pin38", SCK = "GPIO21 - Pin40", CE = "GPIO18 - Pin12 | GPIO17 - Pin11 | GPIO16 - Pin36" },
                            new SPI { CIPO = "GPIO1 - Pin28", COPI = "GPIO2 - Pin3", SCK = "GPIO3 - Pin5", CE = "GPIO0 - Pin27 | GPIO24 - Pin18" },
                            new SPI { CIPO = "GPIO5 - Pin29", COPI = "GPIO6 - Pin31", SCK = "GPIO7 - Pin26", CE = "GPIO4 - Pin7 | GPIO25 - Pin22" },
                            new SPI { CIPO = "GPIO13 - Pin33", COPI = "GPIO14 - Pin8", SCK = "GPIO15 - Pin10", CE = "GPIO12 - Pin31 | GPIO26 - Pin37" },
                            new SPI { CIPO = "GPIO9 - Pin21", COPI = "GPIO10 - Pin19", SCK = "GPIO11 - Pin23", CE = "GPIO8 - Pin24 | GPIO27 - Pin13" }
                        }
                    },
                }
                );
        }

    }
}
