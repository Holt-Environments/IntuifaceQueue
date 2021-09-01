using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace libvideoqv2
{
    public class libvideoqv2 : INotifyPropertyChanged
    {
        #region Attributes

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private String video_1_url = "";
        public String Video1Url
        {
            get { return video_1_url; }
            set
            {
                if (video_1_url != value)
                {
                    video_1_url = value;
                    NotifyPropertyChanged("Video1Url");
                }
            }
        }

        private String video_2_url = "";
        public String Video2Url
        {
            get { return video_2_url; }
            set
            {
                if (video_2_url != value)
                {
                    video_2_url = value;
                    NotifyPropertyChanged("Video2Url");
                }
            }
        }

        private String video_3_url = "";
        public String Video3Url
        {
            get { return video_3_url; }
            set
            {
                if (video_3_url != value)
                {
                    video_3_url = value;
                    NotifyPropertyChanged("Video3Url");
                }
            }
        }

        private String video_4_url = "";
        public String Video4Url
        {
            get { return video_4_url; }
            set
            {
                if (video_4_url != value)
                {
                    video_4_url = value;
                    NotifyPropertyChanged("Video4Url");
                }
            }
        }

        private String video_5_url = "";
        public String Video5Url
        {
            get { return video_5_url; }
            set
            {
                if (video_5_url != value)
                {
                    video_5_url = value;
                    NotifyPropertyChanged("Video5Url");
                }
            }
        }

        public event EventHandler CloseVideoPlayer;
        public event EventHandler PlayVideo1;
        public event EventHandler PlayVideo2;
        public event EventHandler PlayVideo3;
        public event EventHandler PlayVideo4;
        public event EventHandler PlayVideo5;

        private ObservableCollection<String> m_lstVideos;

        private String m_sVideoHead = "";
        public String VideoHead
        {
            get { return m_sVideoHead; }
            set
            {
                if (m_sVideoHead != value)
                {
                    m_sVideoHead = value;
                    NotifyPropertyChanged("VideoHead");
                }
            }
        }

        private enum EnqueueOperation
        {
            NULL,
            START,
            APPEND
        }

        private enum DequeueOperation
        {
            NULL,
            NONE,
            CLEAR,
            EXTRACT,
            EXTRACT_HEAD
        }

        private enum Video
        {
            NULL,
            VIDEO1,
            VIDEO2,
            VIDEO3,
            VIDEO4,
            VIDEO5
        }

#endregion

        public libvideoqv2()
        {
            m_lstVideos = new ObservableCollection<String>();
        }

        #region Triggers

        private Video GetVideoEnum(String temp)
        {
            if (temp == Video1Url)
            {
                return Video.VIDEO1;
            }
            else if (temp == Video2Url)
            {
                return Video.VIDEO2;
            }
            else if (temp == Video3Url)
            {
                return Video.VIDEO3;
            }
            else if (temp == Video4Url)
            {
                return Video.VIDEO4;
            }
            else if (temp == Video5Url)
            {
                return Video.VIDEO5;
            }
            else
            {
                return Video.NULL;
            }
        }

        private void TriggerVideo(Video video_num)
        {
            switch (video_num)
            {
                case Video.VIDEO1:
                    TriggerVideo1();
                    break;
                case Video.VIDEO2:
                    TriggerVideo2();
                    break;
                case Video.VIDEO3:
                    TriggerVideo3();
                    break;
                case Video.VIDEO4:
                    TriggerVideo4();
                    break;
                case Video.VIDEO5:
                    TriggerVideo5();
                    break;
            }
        }

        private void TriggerVideo1()
        {
            if (PlayVideo1 != null)
            {
                PlayVideo1(this, null);
            }
        }

        private void TriggerVideo2()
        {
            if (PlayVideo2 != null)
            {
                PlayVideo2(this, null);
            }
        }

        private void TriggerVideo3()
        {
            if (PlayVideo3 != null)
            {
                PlayVideo3(this, null);
            }
        }

        private void TriggerVideo4()
        {
            if (PlayVideo4 != null)
            {
                PlayVideo4(this, null);
            }
        }

        private void TriggerVideo5()
        {
            if (PlayVideo5 != null)
            {
                PlayVideo5(this, null);
            }
        }

        #endregion

        #region Enqueue

        public void EnqueueVideo1()
        {
            EnqueueVideo(Video1Url);
        }

        public void EnqueueVideo2()
        {
            EnqueueVideo(Video2Url);
        }

        public void EnqueueVideo3()
        {
            EnqueueVideo(Video3Url);
        }

        public void EnqueueVideo4()
        {
            EnqueueVideo(Video4Url);
        }

        public void EnqueueVideo5()
        {
            EnqueueVideo(Video5Url);
        }

        private void EnqueueVideo(String _video_file_url)
        {
            if (m_lstVideos.Contains(_video_file_url)) { return; } // dont enqueue if already queued

            EnqueueOperation op = GetEnqueueOperation();

            switch (op)
            {
                case EnqueueOperation.START:
                    RunStart(_video_file_url);
                    break;
                case EnqueueOperation.APPEND:
                    RunAppend(_video_file_url);
                    break;
            }
        }

        private EnqueueOperation GetEnqueueOperation()
        {
            if (m_lstVideos.Count == 0) { return EnqueueOperation.START; }
            if (m_lstVideos.Count > 0) { return EnqueueOperation.APPEND; }
            return EnqueueOperation.NULL;
        }

        // There must be no elements in the list
        private void RunStart(String _video_file_url)
        {
            m_lstVideos.Add(_video_file_url);
            VideoHead = m_lstVideos[0];

            // Get enum for VideoHead
            Video video_num = GetVideoEnum(VideoHead);

            TriggerVideo(video_num);
        }

        // There must be one or more elements in the list
        private void RunAppend(String _video_file_url)
        {
            m_lstVideos.Add(_video_file_url);
            VideoHead = m_lstVideos[0];
        }

        #endregion

        #region Dequeue

        public void DequeueVideo1()
        {
            DequeueVideo(Video1Url);
        }

        public void DequeueVideo2()
        {
            DequeueVideo(Video2Url);
        }

        public void DequeueVideo3()
        {
            DequeueVideo(Video3Url);
        }

        public void DequeueVideo4()
        {
            DequeueVideo(Video4Url);
        }

        public void DequeueVideo5()
        {
            DequeueVideo(Video5Url);
        }

        private void DequeueVideo(String _video_file_url)
        {
            if (!m_lstVideos.Contains(_video_file_url)) { return; } // If item isnt in list, can't dequeue.

            DequeueOperation op = GetDequeueOperation(_video_file_url); // Item must be in list by this point

            switch (op)
            {
                case DequeueOperation.CLEAR:
                    RunClear(_video_file_url);
                    break;
                case DequeueOperation.EXTRACT:
                    RunExtract(_video_file_url);
                    break;
                case DequeueOperation.EXTRACT_HEAD:
                    RunExtractHead(_video_file_url);
                    break;
            }
        }

        private DequeueOperation GetDequeueOperation(String _video_file_url)
        {
            if (m_lstVideos.Count == 0) { return DequeueOperation.NONE; }
            else if (m_lstVideos.Count == 1) { return DequeueOperation.CLEAR; }
            else
            {
                if (!(m_lstVideos.IndexOf(_video_file_url) == 0)) { return DequeueOperation.EXTRACT; }
                if (m_lstVideos.IndexOf(_video_file_url) == 0) { return DequeueOperation.EXTRACT_HEAD; }
                return DequeueOperation.NULL;
            }
        }

        // There must exist only one element in the list
        private void RunClear(String _video_queue_url)
        {
            m_lstVideos.Remove(_video_queue_url);
            VideoHead = "";

            if (CloseVideoPlayer != null)
            {
                CloseVideoPlayer(this, null);
            }
        }

        // There myst exist more than one element in the list
        // and the head is not being changed.
        private void RunExtract(String _video_queue_url)
        {
            m_lstVideos.Remove(_video_queue_url);
        }

        // List must have more than 1 element
        private void RunExtractHead(String _video_queue_url)
        {
            if (CloseVideoPlayer != null)
            {
                CloseVideoPlayer(this, null);
            }

            m_lstVideos.Remove(_video_queue_url);
            VideoHead = m_lstVideos[0];

            Video video_num = GetVideoEnum(VideoHead);

            TriggerVideo(video_num);
        }

        #endregion
    }
}
