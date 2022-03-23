package com.company;

import java.text.SimpleDateFormat;
import java.time.format.DateTimeFormatter;
import java.time.LocalDateTime;
import java.util.Date;
import java.util.TimeZone;

public class Zadanie5 {

    public static void main(String[] args){
        localTime();
        globalTime();
    }

    public static void localTime(){
        DateTimeFormatter formatter = DateTimeFormatter.ofPattern("dd/MM/yyyy HH:mm:ss");
        LocalDateTime now = LocalDateTime.now();
        System.out.println("Local time: "+formatter.format(now));
    }

    public static void globalTime(){
        SimpleDateFormat simpleDateFormat = new SimpleDateFormat("yyyy-MMM-dd HH:mm:ss");
        simpleDateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
        String date = simpleDateFormat.format(new Date());
        System.out.println("Global time: "+date);
    }
}
