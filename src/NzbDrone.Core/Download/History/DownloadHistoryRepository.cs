﻿using System.Collections.Generic;
using NzbDrone.Core.Datastore;
using NzbDrone.Core.Messaging.Events;

namespace NzbDrone.Core.Download.History
{
    public interface IDownloadHistoryRepository : IBasicRepository<DownloadHistory>
    {
        List<DownloadHistory> FindByDownloadId(string downloadId);
        void DeleteBySeriesId(int seriesId);
    }

    public class DownloadHistoryRepository : BasicRepository<DownloadHistory>, IDownloadHistoryRepository
    {
        public DownloadHistoryRepository(IMainDatabase database, IEventAggregator eventAggregator)
            : base(database, eventAggregator)
        {
        }

        public List<DownloadHistory> FindByDownloadId(string downloadId)
        {
            return Query.Where(h => h.DownloadId == downloadId)
                        .OrderByDescending(h => h.Date);
        }

        public void DeleteBySeriesId(int seriesId)
        {
            Delete(r => r.SeriesId == seriesId);
        }
    }
}
