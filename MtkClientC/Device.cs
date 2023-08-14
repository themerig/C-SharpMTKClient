using System.Collections.Generic;
using System.Windows.Forms;

public class Config
{
    public string watchdog_address { get; set; } = "0x10007000";
    public string uart_base { get; set; } = "0x11002000";
    public string payload_address { get; set; } = "0x100A00";
    public string var_0 { get; set; } = "0x2C";
    public string var_1 { get; set; } = "0xA";
    public string payload_adr { get; set; } 
    public string hw_code_adr { get; set; }
    public string mt_address { get; set; }
    public int crash_method { get; set; } = 0;

    public Config Default(string hw_code)
    {

        Dictionary<string, Config> configData = new Dictionary<string, Config>
        {
            {
                "0x6261",
                new Config
                {
                    mt_address = "mt6735",
                    var_0 = "0x10",
                    var_1 = "0x28",
                    watchdog_address = "0x10212000",
                    payload_adr = "payloads/mt6735_payload.bin",
                }
            },

            {
                "0x0707",
                new Config
                {
                    mt_address = "mt6768",
                    var_0 = "0x2C",
                    var_1 = "0x25",
                    payload_adr = "payloads/mt6768_payload.bin",
                }
            }
           
        };

        string hwCodeHex = "0x" + hw_code.ToString();

        if (configData.TryGetValue(hwCodeHex, out Config configEntry))
        {
            this.watchdog_address = configEntry.watchdog_address;
            this.uart_base = configEntry.uart_base;
            this.payload_address = configEntry.payload_address;
            this.var_0 = configEntry.var_0;
            this.var_1 = configEntry.var_1;
            this.payload_adr = configEntry.payload_adr;
            this.crash_method = configEntry.crash_method;
            this.mt_address = configEntry.mt_address;
            this.hw_code_adr = hwCodeHex;
        }
        else
        {
            throw new KeyNotFoundException($"Can't find {hwCodeHex} hw_code in config");
        }

        return this;
    }
}
