﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicService.ControllerModels
{
    public class SelectedSongCreate
    {
        public long RecordNumber { get; set; }
        public string Track { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Playlist { get; set; }
    }
}