#region <<Notes>>
/*----------------------------------------------------------------
 * Copy right (c) 2024  All rights reserved
 * CLR Ver: 4.0.30319.42000
 * Computer: MOLEQ-MING
 * Company: 
 * namespace: MQPaxWrapper
 * Unique: c384d547-8ae0-4ef3-8a37-5759de14a347
 * File name: CommWrapper
 * Domain: MOLEQ-MING
 * 
 * @author: Ming
 * @email: t8ming@live.com
 * @date: 7/17/2024 11:38:00
 *----------------------------------------------------------------*/
#endregion <<Notes>>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MQVideoPlayer
{
    interface CommWrapper
    {
        void ReportDataReceived(string res);
        void ReportDataReceived(CommResponse resp); 
    }
}
