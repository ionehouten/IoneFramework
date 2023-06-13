using System;

namespace Ione.Framework.Core
{
    /// <summary>
    /// Pagination 
    /// untuk pengaturan paging data
    /// </summary>
    public class Pagination
    {

        public int PageSize
        {
            get;
            set;
        }
        public int TotalData
        {
            get;
            set;
        }
        public int CurrentPage
        {
            get;
            set;
        }

        public int TotalPage
        {
            get
            {
                if (this.TotalData == 0 || this.PageSize == 0) return 0;
                double totalPage = this.TotalData / this.PageSize;
                return (int)Math.Ceiling(totalPage); //Math.ceil = pembulatan ke atas 
            }

        }

        public int CurrentItem
        {
            get
            {
                return CurrentPage * PageSize;
            }
        }

        public void PrevPage()
        {
            CurrentPage--;
        }
        public void NextPage()
        {
            CurrentPage++;
        }

        public void FirstPage()
        {
            CurrentPage = 0;
        }
        public void LastPage()
        {
            CurrentPage = TotalPage;
        }
        
        public bool IsHasNextPage
        {

            get
            {
                if (CurrentPage < TotalPage)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
        public bool IsHasPrevPage
        {
            get
            {
                if (CurrentPage != 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
