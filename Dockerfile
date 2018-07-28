FROM jekyll/jekyll:pages

EXPOSE 4000

WORKDIR docs

CMD [ "jekyll", "serve", "--watch", "--force_polling", "--incremental" ]