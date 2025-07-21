#!/bin/bash

# 1. 이동할 디렉토리 (클론한 프로젝트)
cd ~/레포명 || exit

# 2. git pull 실행 (로그를 파일에 저장)
git pull origin main >> ~/auto_pull.log 2>&1