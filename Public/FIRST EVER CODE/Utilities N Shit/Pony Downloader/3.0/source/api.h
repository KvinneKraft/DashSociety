namespace Downloader_API {
   struct API_STORAGE {
	   HRESULT download_filter;
	  std::string arg, output, url, buffer;
	  bool token;
	  char network_buffer[2500];
   } api;
}
