package com.misideStart.wallpaper;

import android.app.WallpaperManager;
import android.content.ComponentName;
import android.content.Intent;
import android.service.wallpaper.WallpaperService;
import android.util.Log;
import android.view.MotionEvent;
import android.view.SurfaceHolder;

import com.unity3d.player.UnityPlayer;
import com.unity3d.player.UnityPlayerForActivityOrService;
import com.unity3d.player.UnityPlayerGameActivity;

public class UnityWallpaperService extends WallpaperService {

    public static void SetWallpaper()
    {
        Log.i(TAG,"SetWallpaper");
        Intent intent = new Intent();
        intent.setAction(WallpaperManager.ACTION_CHANGE_LIVE_WALLPAPER);
        intent.putExtra(WallpaperManager.EXTRA_LIVE_WALLPAPER_COMPONENT,new ComponentName("com.misideStart.wallpaper", "com.misideStart.wallpaper.UnityWallpaperService"));
        // 检查是否有 Activity 能处理此 Intent
        if (intent.resolveActivity(UnityPlayerGameActivity.instance.getPackageManager()) != null) {
            UnityPlayerGameActivity.instance.startActivity(intent);
        } else {
            // 备用方案：普通动态壁纸选择器
            UnityPlayerGameActivity.instance.startActivity(new Intent(WallpaperManager.ACTION_LIVE_WALLPAPER_CHOOSER));
        }
    }
    private static final String TAG = "UnityWallpaperService";
    private UnityPlayerForActivityOrService service;
    private UnityWallpaperEngine engine;
    @Override
    public Engine onCreateEngine() {
        engine =  new UnityWallpaperEngine();
        service = new UnityPlayerForActivityOrService(getApplicationContext());
        engine.service = service;
        return engine;
    }

    private class UnityWallpaperEngine extends Engine {
        public UnityPlayerForActivityOrService service;
        private boolean isVisible = false;

        @Override
        public void onCreate(SurfaceHolder holder) {
            super.onCreate(holder);
            boolean isPreview = isPreview();
            Log.d(TAG, "Engine onCreate isPreview:"+isPreview);
            if(!isPreview)
            {
                try {
                    UnityPlayer.UnitySendMessage("AndroidSetting","HideSetting",null);
                }catch (Exception ignored){
                }
            }
            setTouchEventsEnabled(true);
            try {
                if (holder.getSurface() != null && holder.getSurface().isValid()) {
                    service.displayChanged(0, holder.getSurface());
                    service.windowFocusChanged(true);
                }
            } catch (Exception e) {
                Log.e(TAG, "Failed to initialize UnityPlayer", e);
            }
        }

        @Override
        public void onTouchEvent(MotionEvent motionEvent) {
            if (service != null) {
                try {
                    service.injectEvent(motionEvent);
                } catch (Exception e) {
                    Log.e(TAG, "Failed to inject touch event", e);
                }
            }
        }

        @Override
        public void onVisibilityChanged(boolean visible) {
            super.onVisibilityChanged(visible);
            Log.i(TAG, "Visibility changed: " + visible);
            isVisible = visible;

            if (service != null) {
                try {
                    if (visible) {
                        service.onResume();
                        SurfaceHolder holder = getSurfaceHolder();
                        if (holder != null && holder.getSurface() != null && holder.getSurface().isValid()) {
                            service.displayChanged(0, holder.getSurface());
                        }
                    } else {
                        service.onPause();
                    }
                    service.windowFocusChanged(visible);
                } catch (Exception e) {
                    Log.e(TAG, "Error during visibility change", e);
                }
            }
        }

        @Override
        public void onDestroy() {
            super.onDestroy();
            Log.d(TAG, "Engine onDestroy");
            if (service != null) {
                try {
                    service.onUnityPlayerQuitted();
                    service = null; // Help GC
                } catch (Exception e) {
                    Log.e(TAG, "Failed to destroy UnityPlayer", e);
                }
            }
        }
    }
}