﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using mshtml;
using CustomUIControls;

namespace robot
{
    public partial class MainForm : DevComponents.DotNetBar.Metro.MetroForm
    {
        HtmlDocument doc = null;

        public static string userName = "";

        string bookReviewUrl = "";

        string phoneNum = ""; // 스터디룸 예약을 위한 변수
        string email = ""; // 스터디룸 예약을 위한 변수
        string thisYear = "2013";

        Random r = new Random();

        /****************************/
        bool isFirstLoading = true;

        Portal portal;
        int currentBoardId = 1;
        BB bb;
        Library library;

        static public string mailCookie = "";
        static public bool mailFormExist = false;

        static public DataGridView gridView;
        static public WebBrowser brows;
        static public Panel bbpanel;

        static public string bbCookie = "";
        /****************************/

        public MainForm()
        {
            InitializeComponent();

            visibleLoading();

            // 브라우저 스크립트 에러 무시
            browser.ScriptErrorsSuppressed = true;

            brows = this.browser;
            gridView = this.boardGrid;
            bbpanel = this.bbPanel;

            autoLoginSetup();

            browser.Navigate("https://portal.unist.ac.kr/EP/web/login/unist_acube_login_int.jsp");

            // 책 검색 옵션 초기화
            bookOption1.SelectedIndex = 0;
            bookOption2.SelectedIndex = 1;
            bookOperator.SelectedIndex = 0;
            roomNumberBox.SelectedIndex = 0;

            circularProgress1.IsRunning = true;
        }

        /**********************************************************
         * 
         *  자동 로그인 체크 박스
         *  
         **********************************************************/

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isFirstLoading == true)
                return;

            CheckBox check = (CheckBox)sender;
            
            if (check.Checked == true)
            {
                DialogResult result = MessageBox.Show("개인정보가 유출될 수 있습니다.\r\n자동 로그인을 하시겠습니까? :[", "Robot의 경고", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                if (result == DialogResult.No)
                {
                    check.Checked = false;
                    return;
                }

                Program.ini.SetIniValue("Login", "Auto", "true");
                Program.ini.SetIniValue("Login", "Save", "true");
                Program.ini.SetIniValue("Login", "Id", Program.id);
                Program.ini.SetIniValue("Login", "Password", Program.password);
            }
            if (check.Checked == false)
            {
                Program.ini.SetIniValue("Login", "Auto", "false");
                Program.ini.SetIniValue("Login", "Save", "false");
                Program.ini.SetIniValue("Login", "Id", "");
                Program.ini.SetIniValue("Login", "Password", "");
            }
        }

        private void autoLoginSetup()
        {
            // 아이디 ini에 저장
            if (Program.loginSave == true)
            {
                Program.ini.SetIniValue("Login", "Id", Program.id);
                Program.ini.SetIniValue("Login", "Save", "true");

                if (Program.autoLogin == true)
                {
                    Program.ini.SetIniValue("Login", "Auto", "true");
                    Program.ini.SetIniValue("Login", "Password", Program.password);
                    checkBox1.Checked = true;
                }
                else
                {
                    Program.ini.SetIniValue("Login", "Auto", "false");
                    Program.ini.SetIniValue("Login", "Password", "");
                    checkBox1.Checked = false;
                }
            }
            else
            {
                Program.ini.SetIniValue("Login", "Id", "");
                Program.ini.SetIniValue("Login", "Save", "false");
            }
        }

        /**********************************************************
         * 
         *  게시판 boardGrid
         *  
         **********************************************************/

        private void showBoardGrid(int boardId)
        {
            while (boardGrid.Rows.Count != 0)
            {
                boardGrid.Rows.RemoveAt(0);
            }

            currentBoardId = boardId;
            PortalBoard[] boards=portal.getBoard(boardId);
            int i = 0;

            if (boardId == 4)
            {
                this.Column5.Width = 0;
            }

            else
            {
                this.Column5.Width = 33;
            }

            foreach (PortalBoard b in boards)
            {
                if (boardId == 4)
                {
                    if (b.rows == null)
                        break;
                    boardGrid.Rows.Add(b.rows);
                    continue;
                }

                else
                {
                    boardGrid.Rows.Add(b.rows);

                    // 셀 글자 색 추가
                    if (b.color != Color.Black)
                    {
                        // 글자 볼드 추가
                        if (b.bold == true)
                        {
                            boardGrid.Rows[i].Cells[1].Style = new DataGridViewCellStyle
                            {
                                ForeColor = b.color,
                                Font = new Font(boardGrid.DefaultCellStyle.Font, FontStyle.Bold)
                            };
                        }
                        else
                        {
                            boardGrid.Rows[i].Cells[1].Style = new DataGridViewCellStyle { ForeColor = b.color };
                        }
                    }

                    // 글자 볼드 추가
                    if (b.bold == true)
                    {
                        boardGrid.Rows[i].Cells[1].Style.Font = new Font(boardGrid.DefaultCellStyle.Font, FontStyle.Bold);
                    }

                    i++;
                }
            }

            browser.Navigate("http://portal.unist.ac.kr/EP/web/collaboration/bbs/jsp/BB_BoardView.jsp?boardid="
                + portal.getBoardId(currentBoardId) + "&bullid=" + portal.getBoardbullId(currentBoardId, 0));
        }

        private void boardGrid_SelectionChanged(object sender, EventArgs e)
        {
            DataGridView grid = (DataGridView)sender;

            if (grid.SelectedRows.Count == 0)
                return;

            if (currentBoardId == 4)
            {
                browser.Navigate("http://portal.unist.ac.kr/EP/web/collaboration/bbs/jsp/BB_BoardView.jsp?boardid="
                   + portal.getBoardId(currentBoardId, grid.SelectedRows[0].Index) + "&bullid=" + portal.getBoardbullId(currentBoardId, grid.SelectedRows[0].Index));
            }
            else
            {
                browser.Navigate("http://portal.unist.ac.kr/EP/web/collaboration/bbs/jsp/BB_BoardView.jsp?boardid="
                    + portal.getBoardId(currentBoardId) + "&bullid=" + portal.getBoardbullId(currentBoardId, grid.SelectedRows[0].Index));
            }
        }

        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            /**********************************************************
             * 
             *  브라우저 오른쪽으로 이동
             *  
             **********************************************************/

            Point point = new Point(browser.Right, 0);
            browser.Document.Window.ScrollTo(point);
            
            if(isFirstLoading==false)
                browser.Visible = true;

            /**********************************************************
             * 
             *  첫 로그인, 이름 저장, 학사 공지로 이동
             *  
             **********************************************************/

            if (e.Url.ToString() == "http://portal.unist.ac.kr/EP/web/portal/jsp/EP_Default1.jsp") {
                userName=browser.DocumentTitle.ToString().Split('-')[1].Split('/')[0];
                welcomeLabel.Text = userName + " 님 환영합니다 :-)";

                portal = new Portal(browser.Document.Cookie);
                showBoardGrid(1);
                browser.Navigate("http://portal.unist.ac.kr/EP/tmaxsso/runUEE.jsp?host=bb");
            }

            /**********************************************************
             * 
             *  로그인 창에서 변수 입력
             *  
             **********************************************************/

            if (e.Url.ToString() == "https://portal.unist.ac.kr/EP/web/login/unist_acube_login_int.jsp")
            {
                doc = browser.Document as HtmlDocument;

                doc.GetElementById("id").SetAttribute("value", Program.id);
                doc.GetElementsByTagName("input")["UserPassWord"].SetAttribute("value", Program.password);
                doc.InvokeScript("doLogin");
            }

            /**********************************************************
             * 
             *  블랙보드
             *
             **********************************************************/

            if (e.Url.ToString() == "http://bb.unist.ac.kr/webapps/portal/frameset.jsp")
            {
                bb=new BB(browser);
            }

            else if (e.Url.ToString() == "http://bb.unist.ac.kr/webapps/blackboard/execute/announcement?method=search&context=mybb&handle=my_announcements")
            {
                bb.setBoard();

                bb.getCourceMenu();
                
                DevComponents.DotNetBar.ButtonItem[] bblist = new DevComponents.DotNetBar.ButtonItem[bb.board.Count()];

                for (int i = 0; i < bb.board.Length; i++)
                {
                    if (bb.board[i] == null)
                        break;
                    bblist[i] = new DevComponents.DotNetBar.ButtonItem();
                    bblist[i].ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
                    bblist[i].CanCustomize = false;
                    bblist[i].Name = "buttonItem" + Convert.ToInt32(i);
                    bblist[i].Text = bb.board[i].name;
                    bblist[i].Click += new System.EventHandler(sideBBClick);
                    bblist[i].Click += new System.EventHandler(visibleBB);

                    slideBB.SubItems.Add(bblist[i]);
                }
                
                browser.Navigate("http://library.unist.ac.kr/DLiWeb25Eng/tmaxsso/first_cs.aspx");
            }

            /**********************************************************
             * 
             *  도서관
             *
             **********************************************************/

            if (e.Url.ToString().IndexOf("http://library.unist.ac.kr/DLiWeb25Eng/default.aspx") != -1)
            {
                library = new Library(browser.Document.Cookie);

                browser.Navigate("http://portal.unist.ac.kr/EP/web/security/jsp/SSO_unistMail.jsp");
            }

            /**********************************************************
             * 
             *  전자우편
             *
             **********************************************************/

            if (e.Url.ToString().IndexOf("http://mail.unist.ac.kr/mail/mailList.crd") != -1)
            {
                circularProgress1.IsRunning = false;
                circularProgress1.Visible = false;

                System.Web.UI.WebControls.GridViewSelectEventArgs ee = new System.Web.UI.WebControls.GridViewSelectEventArgs(1);

                mailCookie = browser.Document.Cookie;

                boardGrid_SelectionChanged(boardGrid, ee);

                visiblePortal();

                isFirstLoading = false;
                circularProgress1.IsRunning = false;
                visiblePortal();

                notifyTimer.Start();
            }
        }

        /**********************************************************
         * 
         *  boardSlide 에서 블랙보드 게시판 클릭시 이벤트
         *  
         **********************************************************/

        private void sideBBClick(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonItem butItem = (DevComponents.DotNetBar.ButtonItem)sender;

            for(int i=0; i<bb.board.Length; i++) {
                if (bb.board[i].name == butItem.Text)
                {
                    browser.Navigate(bb.board[i].menuUrl[0]);
                }
            }
        }

        /**********************************************************
         * 
         *  설정
         *  
         **********************************************************/
        
        private void settingBox_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Designed by Kim Tae Hoon ಠ_ಠ");
        }

        /**********************************************************
         * 
         *  메일
         *  
         **********************************************************/

        private void mailBox_Click(object sender, EventArgs e)
        {
            if (mailFormExist == true)
            {
                MessageBox.Show("창이 이미 열려 있습니다 :(", "Robot의 경고");
                return;
            }

            if (mailFormExist == false)
                mailFormExist = true;


            if (mailCookie == "")
            {
                MessageBox.Show("메일 서버에 접속할 수 없습니다 -_-?", "Robot의 경고");
                return;
            }

            MailForm mailForm=new MailForm(mailCookie);
            mailForm.Show();
        }

        /**********************************************************
         * 
         *  스터디 룸
         *  
         **********************************************************/

        private void loadStudyRoomStat()
        {
            library.loadStudyroomStatus(roomNumberBox.SelectedIndex + 1);

            while (studyGrid.Rows.Count != 0)
            {
                studyGrid.Rows.RemoveAt(0);
            }

            // study room grid 내용 추가
            for (int i = 0; i < library.dayCount; i++)
            {
                studyGrid.Rows.Add(library.roomStat[i]);

                // 오늘 날짜 줄 표시
                if (Convert.ToInt32(library.roomStat[i][0].Substring(0, 2)) == DateTime.Now.Month && Convert.ToInt32(library.roomStat[i][0].Substring(3)) == DateTime.Now.Day)
                    studyGrid.Rows[i].DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.PowderBlue };
            }
        }

        private void roomNumberBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFirstLoading == true)
                return;

            loadStudyRoomStat();
        }

        // study room gridv
        private void studyGrid_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;
            DataGridViewRow row = grid.Rows[grid.Rows.Count - 1];

            for (int i = 0; i < row.Cells.Count; i++)
            {
                if (row.Cells[i].Value == "E")
                {
                    row.Cells[i].Style = new DataGridViewCellStyle { ForeColor=Color.White, BackColor=Color.CadetBlue };
                }
                if (row.Cells[i].Value == "R")
                {
                    row.Cells[i].Style = new DataGridViewCellStyle { ForeColor = Color.White, BackColor = Color.IndianRed };
                }
            }
        }

        private void studyGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (browser.Url.ToString().IndexOf("http://library.unist.ac.kr/DLiWeb25Eng/studyroom/reserve.aspx?m_var=112&roomid=1&rdate=") != -1)
            {
                browser.Navigate("http://library.unist.ac.kr/dliweb25/studyroom/detail.aspx?m_var=112&roomid=" + (roomNumberBox.SelectedIndex + 1).ToString());
            }
            DataGridView grid = (DataGridView)sender;
            studyGroup.Enabled = true;

            string hour = grid.Columns[grid.SelectedCells[0].ColumnIndex].HeaderText.ToString();
            string date = grid.Rows[grid.SelectedCells[0].RowIndex].Cells[0].Value.ToString().Replace("-", "");

            doc.InvokeScript("fnReserve", new object[] { thisYear + date, hour });

            studyDateLabel.Text = thisYear + "년 " + date.Substring(0, 2) + "월 " + date.Substring(2) + "일 " + hour + "시 ~ ";
        }

        /**********************************************************
         * 
         *  책 검색
         *  
         **********************************************************/

        private void bookSearch_Click(object sender, EventArgs e)
        {
            // 도서 상태 초기화
            while (bookGridView.Rows.Count != 0)
            {
                bookGridView.Rows.RemoveAt(0);
            }

            bookTitle.Text = "책 제목";
            bookReview.Text = "";
            bookReviewUrl = "";

            library.bookSearch(bookQuery1.Text, bookQuery2.Text, bookOption1.Text, bookOption2.Text, bookOperator.Text);

            if (library.books.Length == 0)
            {
                MessageBox.Show("조건에 맞는 책이 없습니다 :(", "Robot의 경고");
            }

            for (int i = 0; i < library.books.Length; i++)
            {
                bookGridView.Rows.Add(library.books[i].rows);
            }
        }

        /**********************************************************
         * 
         *  책 검색 옵션, 한쪽 고르면 다른 쪽에서 그 옵션 제거
         *  
         **********************************************************/

        private void bookOption1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (bookOption2.SelectedItem != null)
            {
                if (bookOption2.SelectedItem.ToString() == comboBox.SelectedItem.ToString())
                {
                    MessageBox.Show("두 옵션이 같습니다 >:[", "Robot의 경고");
                }
            }
        }

        private void bookOption2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;

            if (bookOption1.SelectedItem != null)
            {
                if (bookOption1.SelectedItem.ToString() == comboBox.SelectedItem.ToString())
                {
                    MessageBox.Show("두 옵션이 같습니다 >:[", "Robot의 경고");
                }
            }
        }

        /**********************************************************
         * 
         *  책 검색에서 엔터키 입력
         *  
         **********************************************************/

        private void bookQuery1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bookSearch_Click(sender, e);
            }
        }

        private void bookQuery2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bookSearch_Click(sender, e);
            }
        }

        /**********************************************************
         * 
         *  bookGridView 에서 select
         *  
         **********************************************************/
        
        private void bookGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (library.books == null)
                return;

            if (bookGridView.SelectedRows.Count == 0)
                return;

            bookThumnailRefresh();

            loadBookStat(library.books[bookGridView.SelectedRows[0].Index].cid);
            loadBookReview(library.books[bookGridView.SelectedRows[0].Index].isbn);
        }

        private void bookThumnailRefresh() 
        {
            bookPic.Visible = false;
            bookReviewUrl = "";
            bookReview.Text = "...";

            if (bookGridView.SelectedRows.Count == 0)
                return;

            string url=library.books[bookGridView.SelectedRows[0].Index].thumbnail;

            if (url != "")
            {
                bookPic.Load("http://library.unist.ac.kr/DLiWeb25/comp/search/thumb.axd?url=" + url.Split('=')[1]);
                bookPic.Visible = true;
            }

            Book book = library.books[bookGridView.SelectedRows[0].Index];

            if (book.title.Length > 30)
            {
                bookTitle.Text = book.title.ToString().Substring(0, 30) + "\r\n" + book.title.ToString().Substring(30);
            }
            else {
                bookTitle.Text = book.title.ToString();
            }

            bookInfo.Text = book.author + " | " + book.publisher + " | " + book.publishYear + " | " + book.kind;

            //http://openapi.naver.com/search?key=6053ca2ccd452f386a6e2eb44375d160&query=art&target=book_adv&d_isbn=9788996427513
        }
        
        private void loadBookStat(string cid)
        {
            while (bookListGrid.Rows.Count != 0)
            {
                bookListGrid.Rows.RemoveAt(0);
            }

            Column37.Width = 80;

            string[] str = library.loadBookStat(cid);
            int count = str.Length / 4;

            for (int i = 0; i < count; i++)
            {
                string[] subStr=new string[4];

                Array.Copy(str, i * 4, subStr, 0, 4);
                bookListGrid.Rows.Add(subStr);

                if (subStr[0].IndexOf("대출중") != -1)
                {
                    Column37.Width = subStr[0].Length * 8;
                }
            }
        }

        private void loadBookReview(string isbn) 
        {
            bookReviewUrl = "";
            bookReview.Text = "...";
            reviewStar.Visible = false;

            string url = library.loadBookReview(isbn);

            reviewBrowser.Navigate(url);
        }

        /**********************************************************
         * 
         *  책 리뷰 보기 클릭
         *  
         **********************************************************/

        private void bookReviewBtn_Click(object sender, EventArgs e)
        {
            if (bookReviewUrl == "")
            {
                MessageBox.Show("리뷰가 없습니다 :-(", "Robot의 경고");
            }
            else
            {
                System.Diagnostics.Process.Start(bookReviewUrl);
            }
        }

        /**********************************************************
         * 
         *  리스트 클릭 이벤트 함수
         *  
         **********************************************************/

        // 학사 공지
        private void buttonItem1_Click(object sender, EventArgs e)
        {
            visiblePortal();
            showBoardGrid(1);
        }

        // 전체 공지
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            visiblePortal();
            showBoardGrid(2);
        }

        // 대학원 공지
        private void buttonItem3_Click(object sender, EventArgs e)
        {
            visiblePortal();
            showBoardGrid(3);
        }

        // 최신 게시글
        private void buttonItem4_Click(object sender, EventArgs e)
        {
            visiblePortal();
            showBoardGrid(4);
        }

        // 도서 검색
        private void buttonItem5_Click(object sender, EventArgs e)
        {
            visibleBookSearch();
        }

        // library 예약
        private void buttonItem6_Click(object sender, EventArgs e)
        {
            loadStudyRoomStat();
            visibleStudyroomReserve();
        }

        // 열람실 좌석 현황
        private void buttonItem7_Click(object sender, EventArgs e)
        {

        }

        /**********************************************************
         * 
         *  리스트 별 visible 세팅
         *  
         **********************************************************/

        private void visibleLoading()
        {
            boardSlide.Visible = false;
            browser.Visible = false;
            boardGrid.Visible = false;
            bbPanel.Visible = false;
            studyGroup.Visible = false;
            bookGroup.Visible = false;
            bookInfoGroup.Visible = false;
            studyGrid.Visible = false;
            roomNumberLabel.Visible = false;
            roomNumberBox.Visible = false;
        }

        private void visiblePortal()
        {
            boardSlide.Visible = true;
            browser.Visible = true;
            boardGrid.Visible = true;

            bbPanel.Visible = false;
            studyGroup.Visible = false;
            bookGroup.Visible = false;
            bookInfoGroup.Visible = false;
            studyGrid.Visible = false;
            roomNumberLabel.Visible = false;
            roomNumberBox.Visible = false;
        }

        private void visibleBB(object sender, EventArgs e)
        {
            visibleBB();
        }

        public void visibleBB()
        {
            boardSlide.Visible = true;
            browser.Visible = true;
            bbPanel.Visible = true;

            boardGrid.Visible = false;
            studyGroup.Visible = false;
            bookGroup.Visible = false;
            bookInfoGroup.Visible = false;
            studyGrid.Visible = false;
            roomNumberLabel.Visible = false;
            roomNumberBox.Visible = false;
        }

        private void visibleBookSearch()
        {
            boardSlide.Visible = true;
            bookGroup.Visible = true;
            bookInfoGroup.Visible = true;

            bbPanel.Visible = false;
            browser.Visible = false;
            boardGrid.Visible = false;
            studyGroup.Visible = false;
            studyGrid.Visible = false;
            roomNumberLabel.Visible = false;
            roomNumberBox.Visible = false;
        }

        private void visibleStudyroomReserve()
        {
            boardSlide.Visible = true;
            studyGrid.Visible = true;
            roomNumberLabel.Visible = true;
            roomNumberBox.Visible = true;
            studyGroup.Visible = true;

            bbPanel.Visible = false;
            browser.Visible = false;
            boardGrid.Visible = false;
            bookGroup.Visible = false;
            bookInfoGroup.Visible = false;
        }

        /**********************************************************
         * 
         *  브라우저 팝업창 disable
         *  
         **********************************************************/

        private void browser_NewWindow(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        /**********************************************************
         * 
         *  네이버 리뷰 점수, reviewBrowser 사용
         *  
         **********************************************************/

        private void reviewBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (e.Url.ToString().IndexOf("http://book.naver.com/bookdb/book_detail.nhn?bid=") != -1)
            {
                doc = reviewBrowser.Document as HtmlDocument;

                bookReview.Text = "";
                string html = reviewBrowser.Document.Body.InnerHtml;
                bookReviewUrl = "";

                HtmlElement element = ElementsByClass(doc, "txt_desc").ElementAt(0);

                string str = element.GetElementsByTagName("strong")[0].InnerText;
                bookReview.Text += str.Substring(0, str.IndexOf('.'));
                bookReview.Text += str.Substring(str.IndexOf('.'), 2);
                //bookReview.Text += html.Substring(html.IndexOf("네티즌리뷰")).Split('건')[0];
                bookReview.Text += " (" + element.InnerText.Split('|')[1].Substring(6) + ")";
                bookReviewUrl = element.GetElementsByTagName("a")[0].GetAttribute("href");

                reviewStar.Visible = true;
                reviewStar.Rating = (int)(Convert.ToDouble(element.GetElementsByTagName("strong")[0].InnerText) / 2);
            }
        }

        static IEnumerable<HtmlElement> ElementsByClass(HtmlDocument doc, string className)
        {
            foreach (HtmlElement e in doc.All)
                if (e.GetAttribute("className") == className)
                    yield return e;
        }

        /**********************************************************
         * 
         *  RGB string 을 Color 객체로 변환
         *  
         **********************************************************/

        private Color ConvertColor_PhotoShopStyle_toRGB(string photoShopColor)
        {
            int red, green, blue;
            red = Convert.ToInt32(Convert.ToByte(photoShopColor.Substring(1, 2), 16));
            green = Convert.ToInt32(Convert.ToByte(photoShopColor.Substring(3, 2), 16));
            blue = Convert.ToInt32(Convert.ToByte(photoShopColor.Substring(5, 2), 16));

            return Color.FromArgb(red, green, blue);
        }

        /**********************************************************
         * 
         *  알림 타이머
         *  
         **********************************************************/

        private void notifyTimer_Tick(object sender, EventArgs e)
        {
            if (isFirstLoading == true)
                return;
            else
                portal.checkNewLastestBoard();
        }

        /**********************************************************
         * 
         *  메인폼 로드시 트레이 아이콘의 ContextMenuStrip 연결
         *  
         **********************************************************/

        private void MainForm_Load(object sender, EventArgs e)
        {
            trayIcon.ContextMenuStrip = menuStrip;
        }

        /**********************************************************
         * 
         *  닫기 버튼 이벤트 취소
         *  
         **********************************************************/

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;

            this.Visible = false;
        }

        /**********************************************************
         * 
         *  트레이 아이콘 관련 함수
         *  
         **********************************************************/

        private void 보이기ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Visible = true;
        }

        private void 종료ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trayIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
        }

        /**********************************************************
         * 
         *  시작 프로그램 등록 함수
         *  
         **********************************************************/

        private void SetStartup(string AppName, bool enable)
        {
            string runKey = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            Microsoft.Win32.RegistryKey startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey);

            if (enable)
            {
                if (startupKey.GetValue(AppName) == null)
                {
                    // 시작프로그램에 등록(Add startup reg key)
                    startupKey.Close();
                    startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                    startupKey.SetValue(AppName, Application.ExecutablePath.ToString());
                    startupKey.Close();
                }
            }
            else
            {
                // 시작프로그램에서 삭제(remove startup)
                startupKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(runKey, true);
                startupKey.DeleteValue(AppName, false);
                startupKey.Close();
            }
        }

        private void browser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            browser.Visible = false;
        }
    }
}
