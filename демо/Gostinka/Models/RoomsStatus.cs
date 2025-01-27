using System;
using System.Collections.Generic;

namespace Gostinka.Models;

public partial class RoomsStatus
{
    public int IdRoomStatus { get; set; }

    public int RoomId { get; set; }

    public int StatusId { get; set; }

    public int? StatusDate { get; set; }

    public virtual Room Room { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
